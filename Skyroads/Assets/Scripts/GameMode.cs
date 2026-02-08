using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private UIController _uiController;

    public UnityEvent StartGameEvent;
    public UnityEvent EndGameEvent;

    private bool _isGamePaused = false;

    private void Start()
    {
        _uiController.OnGameLoad();
        PauseGame();
    }

    private void Update()
    {
        // Waiting for a any key press to start the game
        if (_isGamePaused && Input.GetKey(KeyCode.Space))
        {
            PauseGame();
            StartGameEvent?.Invoke();
            _uiController.OnGameStart();
        }

        if (Input.GetKey(KeyCode.Escape))
        { 
            Application.Quit();
        }
    }
    public void OnCrash()
    {
        _uiController.OnGameEnd();
        Time.timeScale = 0;
        EndGameEvent.Invoke();
    }

    private void PauseGame()
    {
        _isGamePaused = !_isGamePaused;
        Time.timeScale = _isGamePaused ? 0 : 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
