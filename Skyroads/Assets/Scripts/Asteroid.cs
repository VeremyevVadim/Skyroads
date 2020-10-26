using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Degrees per second")]
    private float _rotationSpeed = 180;

    [SerializeField]
    private Ship _ship = null;

    private Transform _transform;

    private const float _speedCoefficient = 40;

    private void Start()
    {
        _ship = FindObjectOfType<Ship>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Quaternion rotationY = Quaternion.AngleAxis(_rotationSpeed * Time.unscaledDeltaTime, Vector3.up);
        transform.rotation *= rotationY;
        
        // Move with ship forward speed
        if(_ship != null)
            _transform.Translate( new Vector3(0, 0, - Time.deltaTime * (_ship.ForwardSpeed * _speedCoefficient)), Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Catcher":
                Destroy(gameObject);
                break;
            case "Path":
                Destroy(gameObject);
                break;
            case "Asteroid":
                Destroy(gameObject);
                break;
            default:
                return;
        }
    }
}
