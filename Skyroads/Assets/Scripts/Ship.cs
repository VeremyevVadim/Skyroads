using Settings;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private ShipSettings _shipSettings;

    [SerializeField]
    private LevelSettings _levelSettings;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _crashClip;

    [SerializeField]
    private SoundController _soundController ;

    // Returns TurboSpeed if Space bar pressed
    public float ForwardSpeed => IsSpeedUp ? _shipSettings.TurboSpeed : _shipSettings.ForwardSpeed;
    public bool IsSpeedUp { get; private set; } = false;

    public UnityEvent CrashEvent;

    private float _roadHalfWidth;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;

        _transform.position = new Vector3(_transform.position.x, _levelSettings.FlyHeight, _transform.position.z);
        _roadHalfWidth = _levelSettings.RoadWidth / 2;
    }

    private void FixedUpdate()
    {
        Move();
        SpeedUp();
        _transform.rotation = Quaternion.Euler(0, 0, -_rigidbody.linearVelocity.x * _shipSettings.RotationPower);
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
        var xPosition = _transform.position.x;
        var xDirection = _movementVector.x;

        if ((xPosition < _roadHalfWidth && xDirection > 0) || (xPosition > -_roadHalfWidth && xDirection < 0))
            _rigidbody.AddForce(_movementVector * _shipSettings.HorizontalSpeed, ForceMode.VelocityChange);
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
        if (_audioSource)
        {
            AudioSource.PlayClipAtPoint(_crashClip, _transform.position, _soundController.GetSoundVolume());
        }

        CrashEvent.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Asteroid"))
        {
            Crash();
        }
    }

    public void OnGameStart()
    {
        if (_audioSource)
        {
            _audioSource.Play();
        }
    }
}
