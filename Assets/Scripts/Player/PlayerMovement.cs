using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private PlayerInputsManager inputsManager;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float airMultiplier = 0f;
    [SerializeField] private float groundFriction = 10f;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float groundCheckLength = 0.1f;
    [SerializeField] private float maxSlopeAngle = 40f;

    private bool isGrounded;
    private RaycastHit2D groundHit;
    private bool onSlope;

    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float minJumpTime = 0.1f;
    private float curMinJumpTime;

    private bool canChangeUseGravity = true;
    private bool _uG;
    private bool useGravity
    {
        get
        {
            return _uG;
        }
        set
        {
            if (!canChangeUseGravity) return;
            _uG = value;
            rigidbody.gravityScale = value ? 1f : 0f;
        }
    }

    private bool canChangeLinearDamping = true;
    private float _lD;
    private float linearDamping
    {
        get
        {
            return _lD;
        }
        set
        {
            if(!canChangeLinearDamping) return;
            _lD = value;
            rigidbody.linearDamping = value;
        }
    }

    public Vector2 moveInputs;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inputsManager = GetComponent<PlayerInputsManager>();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded(out groundHit);
        if (isGrounded != this.isGrounded)
        {
            this.isGrounded = isGrounded;

            if (isGrounded)
                OnGrounded();
            else
                OnUngrounded();
        }

        bool onSlope = OnSlope(groundHit);
        if(this.onSlope != onSlope)
        {
            this.onSlope = onSlope;

            if (onSlope)
                OnGotOnSlope();
            else
                OnGotOfSlope();
        }

        if(curMinJumpTime > 0f)
        {
            curMinJumpTime -= Time.deltaTime;
            if (curMinJumpTime <= 0f)
                OnMinJumpTime();
        }
    }

    private bool IsGrounded(out RaycastHit2D raycastHit)
    {
        float length = Mathf.Max(0.5f - groundCheckRadius + groundCheckLength, 0f);
        raycastHit = Physics2D.CircleCast(transform.position, groundCheckRadius, Vector2.down, length, groundLayerMask);
        return raycastHit;
    }

    private bool OnSlope(RaycastHit2D raycastHit)
    {
        float angle = Vector2.Angle(raycastHit.normal, Vector2.up);
        return angle > 0f && angle <= maxSlopeAngle;
    }

    private void OnGrounded()
    {
        linearDamping = groundFriction;
    }

    private void OnUngrounded()
    {
        linearDamping = 0f;
    }

    private void OnGotOnSlope()
    {
        useGravity = false;
    }

    private void OnGotOfSlope()
    {
        useGravity = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDir = new Vector2(moveInputs.x, 0f);

        if (onSlope)
            moveDir = GetSlopeDir(moveDir, groundHit);

        //if (!NeedToAddSpeed(moveDir.normalized)) return;
        rigidbody.AddForce(moveDir.normalized * speed * (isGrounded ? 1f : airMultiplier) * 10f, ForceMode2D.Force);
    }
        
    private bool NeedToAddSpeed(Vector2 moveDir) 
    {
        float dotProduct = Vector2.Dot(moveDir, rigidbody.linearVelocity);
        return rigidbody.linearVelocity.magnitude <= speed && dotProduct <= 0.5f;
    }

    private Vector2 GetSlopeDir(Vector2 moveDir, RaycastHit2D raycastHit)
    {
        return Vector3.ProjectOnPlane(moveDir, raycastHit.normal);
    }

    public void JumpIfCanTo()
    {
        if (CanJump())
            Jump();
    }

    private void Jump()
    {

        linearDamping = 0f;
        canChangeLinearDamping = false;
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        curMinJumpTime = minJumpTime;
    }

    private void OnMinJumpTime()
    {
        canChangeLinearDamping = true;
        linearDamping = isGrounded ? groundFriction : 0f;
    }

    private bool CanJump()
    {
        return isGrounded && curMinJumpTime <= 0f;
    }
}
