using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start game GUI
    [SerializeField]
    private GameObject _startGamePanel = null;
    [SerializeField]
    private Text _pressAnyKey = null;

    // In game GUI
    [SerializeField]
    private GameObject _inGamePanel = null;
    [SerializeField]
    private Text _score = null;
    [SerializeField]
    private Text _bestScore = null;
    [SerializeField]
    private Text _time = null;
    [SerializeField]
    private Text _asteroidCount = null;

    // End game GUI
    [SerializeField]
    private GameObject _endGamePanel = null;
    [SerializeField]
    private Text _beatBestScore = null;
    [SerializeField]
    private Text _gameOver = null;
    [SerializeField]
    private Text _restartGame = null;

    [SerializeField]
    private GameObject _scoreCounterObject = null;

    // _scoreCounter provides game state (current score, best score, number of asteroids passed and game time)
    private ScoreCounter _scoreCounter = null;

    private void Start()
    {
        _pressAnyKey.text = StringValuesEn.PRESS_ANY_KEY;

        _scoreCounter = _scoreCounterObject.GetComponent<ScoreCounter>();
    }

    // Set game state values to ingame GUI
    private void FixedUpdate()
    {
        SetGameValues(_scoreCounter.GameTime, _scoreCounter.Score, _scoreCounter.Asteroids, _scoreCounter.BestScore);
    }

    public void OnGameLoad()
    {
        _startGamePanel.SetActive(true);
        _inGamePanel.SetActive(false);
        _endGamePanel.SetActive(false);
    }

    public void OnGameStart() 
    {
        _startGamePanel.SetActive(false);
        _inGamePanel.SetActive(true);
    }

    public void OnGameEnd()
    {
        _beatBestScore.enabled = _scoreCounter.IsBestScoreBeaten ? true: false;
        _beatBestScore.text = StringValuesEn.NEW_RECORD;

        _gameOver.text = StringValuesEn.GAME_OVER;

        _restartGame.text = StringValuesEn.RESTART;

        _endGamePanel.SetActive(true);
    }

    public void OnGameRestart()
    {
        _endGamePanel.SetActive(false);
        _inGamePanel.SetActive(true);
    }
    
    // Format time from float to MM:SS:mm format
    private string TimeToString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    private void SetGameValues(float time, float score, int asteroidCount, int bestScore)
    {
        _score.text = score.ToScoreString();

        _asteroidCount.text = StringValuesEn.GAME_ASTEROIDS + asteroidCount.ToString();

        _bestScore.text = StringValuesEn.GAME_BEST_SCORE + bestScore.ToString();

        _time.text = StringValuesEn.GAME_TIME + TimeToString(time);
    }

}
