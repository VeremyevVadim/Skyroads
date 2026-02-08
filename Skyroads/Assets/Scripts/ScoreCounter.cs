using Settings;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private Ship _ship;
    
    [SerializeField]
    private AsteroidSpawner _spawner;

    [SerializeField]
    private LevelSettings _levelSettings;

    private float _startGameTime;
    private float _gameTime = 0;

    private float _score = 0;

    public int Score { get { return Mathf.RoundToInt(_score); } }
    public int Asteroids { get; private set; } = 0;
    public int BestScore { get; private set; } = 0;
    public float GameTime { get { return _gameTime - _startGameTime; } }
    public bool IsBestScoreBeaten { get; private set; } = false;

    private int _lastLevelUpAsteroidCount = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(StringValuesEn.PLAYER_PREFS_BEST_SCORE))
        {  
            PlayerPrefs.SetInt(StringValuesEn.PLAYER_PREFS_BEST_SCORE, 0);
        }
        else 
        {
            BestScore = PlayerPrefs.GetInt(StringValuesEn.PLAYER_PREFS_BEST_SCORE);
        }

        _startGameTime = Time.time;
        _gameTime = _startGameTime;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        _score += ( Time.deltaTime * _ship.ForwardSpeed) * _levelSettings.PointsGainPerSecond;

        if (_score > BestScore)
        {
            BestScore = Mathf.RoundToInt(_score);
            IsBestScoreBeaten = true;
        }

        if (Asteroids > _lastLevelUpAsteroidCount && Asteroids % _levelSettings.AsteroidsToLvlUp == 0)
        {
            _spawner.LevelUp();
            _lastLevelUpAsteroidCount = Asteroids;
        }
    }

    private void AsteroidDodge()
    {
        _score += _levelSettings.PointsGainPerAsteroid;
        Asteroids++;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Asteroid"))
        {
            AsteroidDodge();
        }
    }

    public void OnEndGame()
    {
        PlayerPrefs.SetInt(StringValuesEn.PLAYER_PREFS_BEST_SCORE, BestScore);
    }
}
