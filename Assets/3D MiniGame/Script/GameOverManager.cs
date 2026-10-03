using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gameOverPanel;
    private float fallHeight = -5f;
    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
            return;

        if (player == null)
            return;

        if (player.position.y < fallHeight)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void Menu()
    {

    }
}