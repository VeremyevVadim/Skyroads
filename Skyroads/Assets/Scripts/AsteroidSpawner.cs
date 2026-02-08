using Settings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private Asteroid _asteroidPrefab;

    [SerializeField]
    private LevelSettings _levelSettings;

    [SerializeField]
    private Ship _ship;

    private const int FixedUpdateCallsInMinute = 3000;

    private float _spawnFrequency = 0;
    private float _roadHalfWidth;
    private float _flyHeight;

    private Transform _transform;

    private IObjectPool<Asteroid> _pool;
    private readonly List<Asteroid> _activeAsteroids = new List<Asteroid>(50);

    private void Awake()
    {
        _pool = new ObjectPool<Asteroid>(
            createFunc: CreateAsteroid,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyAsteroid,
            collectionCheck: true,
            defaultCapacity: 50,
            maxSize:200
        );
    }

    private Asteroid CreateAsteroid()
    {
        var asteroid = Instantiate(_asteroidPrefab);
        asteroid.Initialize(_ship);
        asteroid.SetActive(false);
        return asteroid;
    }

    private void OnGetFromPool(Asteroid asteroid)
    {
        asteroid.Destroyed += ReturnToPool;
        asteroid.SetActive(true);
        _activeAsteroids.Add(asteroid);
    }

    private void OnReleaseToPool(Asteroid asteroid)
    {
        asteroid.Destroyed -= ReturnToPool;
        asteroid.SetActive(false);
        _activeAsteroids.Remove(asteroid);
    }

    private void OnDestroyAsteroid(Asteroid asteroid)
    {
        Destroy(asteroid.gameObject);
    }

    public void Spawn(Vector3 position)
    {
        var asteroid = _pool.Get();
        asteroid.SetPosition(position);
    }

    public void ReturnToPool(Asteroid asteroid)
    {
        _pool.Release(asteroid);
    }

    private void Start()
    {
        _roadHalfWidth = _levelSettings.RoadWidth / 2;
        _flyHeight = _levelSettings.FlyHeight;
        _spawnFrequency = _levelSettings.SpawnFrequency;

        _transform = transform;
    }

    public void LevelUp()
    {
        _spawnFrequency += _levelSettings.FrequancyPerLvlUp;
    }

    private void Update()
    {
        var deltaTime = Time.unscaledDeltaTime;

        for (int i = _activeAsteroids.Count - 1; i >= 0; i--)
        {
            _activeAsteroids[i].Tick(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // Spawn _spawnFrequence asteroids in 1 minut
        float randomTicket = UnityEngine.Random.Range(0f, 1f);
        if (randomTicket < _spawnFrequency / FixedUpdateCallsInMinute)
        {
            var randomX = UnityEngine.Random.Range(-_roadHalfWidth, _roadHalfWidth);
            Spawn(new Vector3(randomX, _flyHeight, _transform.localPosition.z));
        }
    }
}
