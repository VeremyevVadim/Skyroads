using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 1f;

    [SerializeField]
    private float _forwardSpeed = 1f;

    // Returns x2 speed if Space bar pressed
    public float ForwardSpeed
    {
        get
        {
            if (IsSpeedUp)
                return Mathf.Round(2 * _forwardSpeed);
            else
                return _forwardSpeed;
        }
    }
    
    public bool IsSpeedUp { get; private set; } = false;

    public UnityEvent CrashEvent;

    private const float _rotationPower = 3;
    private float _roadHalfWidth;

    private Rigidbody _rb;
    private Transform _transform;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        // Set flight height
        _transform.position = new Vector3(_transform.position.x, GameMode.FlyHeight, _transform.position.z);

        _roadHalfWidth = GameMode.RoadWidth / 2;
    }

    private void FixedUpdate()
    {
        // Player left/right movement
        Move();

        // Set x2 speed if space bar pressed 
        SpeedUp();

        // Tilt the ship in the movement direction 
        _transform.rotation = Quaternion.Euler(0, 0, -_rb.velocity.x * _rotationPower);
    }

    private Vector3 _movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            return new Vector3(horizontal, 0.0f, 0.0f);
        }
    }

    private void Move()
    {
        float xPosition = _transform.position.x;
        float xDirection = _movementVector.x;

        // Position.X restrictions
        if ((xPosition < _roadHalfWidth && xDirection > 0) || (xPosition > -_roadHalfWidth && xDirection < 0))
            _rb.AddForce(_movementVector * HorizontalSpeed, ForceMode.VelocityChange);
    }

    private void SpeedUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            IsSpeedUp = true;
        }
        else
        {
            IsSpeedUp = false;
        }
    }

    private void Crash()
    {
        CrashEvent.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Crash();
        }
    }
}
