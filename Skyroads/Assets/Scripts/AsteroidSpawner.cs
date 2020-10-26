using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid Target;

    [Tooltip("Spawn \"spawnFrequency\" targets in min")]
    [Range(0, 1000)]
    [SerializeField]
    private float _spawnFrequency = 2;

    private float _roadHalfWidth;
    private float _flyHeight;

    private Transform _transform;
    private void Start()
    {
        _roadHalfWidth = GameMode.RoadWidth / 2;
        _flyHeight = GameMode.FlyHeight;

        _transform = GetComponent<Transform>();
    }

    public void LevelUp()
    {
        _spawnFrequency += 20;
    }

    private void FixedUpdate()
    {
        // Spawn _spawnFrequence asteroids in 1 minut
        float randomTicket = Random.Range(0f, 1f);
        if (randomTicket < _spawnFrequency / 3000) // 3000 calls in min
        {
            Asteroid clone = Instantiate(Target);
            float randomX = Random.Range(-_roadHalfWidth, _roadHalfWidth);
            clone.transform.localPosition = new Vector3(randomX, _flyHeight, _transform.localPosition.z);
        }
    }
}
