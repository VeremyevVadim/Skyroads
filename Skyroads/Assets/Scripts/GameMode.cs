using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public static float RoadWidth = 8f;
    public static float FlyHeight = 1.5f;
    public static int PointsPerSecond = 1;
    public static int PointsPerAsteroid = 5;
    public static int AsteroidsToLvlUp = 20;

    private bool _isGamePaused = false;

    public UnityEvent EndGameEvent;

    [SerializeField]
    private Canvas _canvas = null;

    private UIController _uiController;

    // Method for ship CrashEvent
    public void OnCrash()
    {
        _uiController.OnGameEnd();
        Time.timeScale = 0;
        EndGameEvent.Invoke();
    }

    private void Start()
    {
        _uiController = _canvas.GetComponent<UIController>();
        _uiController.OnGameLoad();
        PauseGame();
    }

    private void Update()
    {
        // Waiting for a any key press to start the game
        if (_isGamePaused && Input.anyKey)
        {
            PauseGame();
            _uiController.OnGameStart();
        }

        if (Input.GetKey(KeyCode.Escape))
        { 
            Application.Quit();
        }
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
