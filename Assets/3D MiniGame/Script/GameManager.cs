using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject endGamePanel;
    private float fallHeight = -5f;
    private bool gameOver = false;
    private bool endGame = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameOver || endGame)
            return;
        if (player == null)
            return;
        if (player.position.y < fallHeight)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }



    public void EndGame()
    {
        if (gameOver || endGame)
            return;
        endGame = true;
        Debug.Log("Game Done");
        endGamePanel.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Menu()
    {

    }
}