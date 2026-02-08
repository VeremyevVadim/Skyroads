using Settings;
using UnityEngine;

// Phantom ship which flies in front of the spawner and create a path for real ship
public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private float _travelSpeed = 2f;

    [SerializeField]
    private LevelSettings _levelSettings;

    private const float Eps = 0.0003f;

    private Transform _transform;

    private float _startX;
    private float _distanceX;
    private float _targetX;
    private float _travelTime = 0;
    private float _roadHalfWidth;

    private void Start()
    {
        _transform = transform;
        _roadHalfWidth = _levelSettings.RoadWidth / 2;

        _transform.position = new Vector3(_transform.position.x, _levelSettings.FlyHeight, _transform.position.z);

        _startX = _transform.position.x;
        _targetX = GetRandomX();
        _distanceX = Mathf.Abs(_startX - _targetX);
    }

    private float GetRandomX()
    {
        return Random.Range(-_roadHalfWidth, _roadHalfWidth);
    }

    private void FixedUpdate()
    {
        // Move to target position
        var currentX = Mathf.Lerp(_startX, _targetX, _travelTime);
        _travelTime += Time.deltaTime / (_distanceX / _travelSpeed);
        _transform.position = new Vector3(currentX, _transform.position.y, _transform.position.z);

        // Checks if the ship has arrived at target position. Create new target if true
        if (Mathf.Abs(currentX - _targetX) < Eps)
        {
            _transform.position = new Vector3(_targetX, _transform.position.y, _transform.position.z);
            _startX = _targetX;
            _targetX = GetRandomX();
            _distanceX = Mathf.Abs(_startX - _targetX);
            _travelTime = 0;
        }
    }
}
