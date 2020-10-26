using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phantom ship which flies in front of the spawner and create a path for real ship
public class PathCreator : MonoBehaviour
{
    private Transform _transform;

    private float _startX;
    private float _distanceX;
    private float _targetX;
    private float _travelTime = 0;

    private const float _Eps = 0.0003f;

    [SerializeField]
    private float _travelSpeed = 2f;

    private float _roadHalfWidth;
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _roadHalfWidth = GameMode.RoadWidth / 2;

        _transform.position = new Vector3(_transform.position.x, GameMode.FlyHeight, _transform.position.z);

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
        float currentX = Mathf.Lerp(_startX, _targetX, _travelTime);
        _travelTime += Time.deltaTime / (_distanceX / _travelSpeed);
        _transform.position = new Vector3(currentX, _transform.position.y, _transform.position.z);

        // Checks if the ship has arrived at target position. Create new target if true
        if (Mathf.Abs(currentX - _targetX) < _Eps)
        {
            _transform.position = new Vector3(_targetX, _transform.position.y, _transform.position.z);
            _startX = _targetX;
            _targetX = GetRandomX();
            _distanceX = Mathf.Abs(_startX - _targetX);
            _travelTime = 0;
        }
    }
}
