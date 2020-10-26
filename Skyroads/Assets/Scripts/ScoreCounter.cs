using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private Ship _ship = null;
    
    [SerializeField]
    private AsteroidSpawner _spawner = null;

    private float _startGameTime;
    private float _gameTime = 0;

    private float _score = 0;

    public int Score { get { return Mathf.RoundToInt(_score); } }
    public int Asteroids { get; private set; } = 0;
    public int BestScore { get; private set; } = 0;
    public float GameTime { get { return _gameTime - _startGameTime; } }
    public bool IsBestScoreBeaten { get; private set; } = false;

    private int _asteroidsToLevelUp;
    private int _pointPerSecond;
    private int _pointPerAsteroid;
    private int _lastLevelUpAsteroidCount = 0;

    private void Start()
    {
        _asteroidsToLevelUp = GameMode.AsteroidsToLvlUp;
        _pointPerSecond = GameMode.PointsPerSecond;
        _pointPerAsteroid = GameMode.PointsPerAsteroid;

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
        _score += (_ship.IsSpeedUp ? Time.deltaTime * 2 : Time.deltaTime) * _pointPerSecond;

        // Update best score
        if (_score > BestScore)
        {
            BestScore = Mathf.RoundToInt(_score);
            IsBestScoreBeaten = true;
        }

        // LevelUp asteroid spawner
        if (Asteroids > _lastLevelUpAsteroidCount && Asteroids % _asteroidsToLevelUp == 0)
        {
            _spawner.LevelUp();
            _lastLevelUpAsteroidCount = Asteroids;
        }
    }

    private void AsteroidDodge()
    {
        _score += _pointPerAsteroid;
        Asteroids++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            AsteroidDodge();
        }
    }

    public void OnEndGame()
    {
        PlayerPrefs.SetInt(StringValuesEn.PLAYER_PREFS_BEST_SCORE, BestScore);
    }
}
