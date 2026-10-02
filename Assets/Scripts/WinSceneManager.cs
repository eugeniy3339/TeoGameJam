using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsCountText;

    private void Awake()
    {
        coinsCountText.text = Player.coinsCount.ToString() + "X";
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(0);
    }
}
