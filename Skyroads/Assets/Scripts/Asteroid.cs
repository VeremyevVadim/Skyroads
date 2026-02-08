using Settings;
using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private AsteroidSettings _asteroidSettings;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshCollider _meshCollider;

    public event Action<Asteroid> Destroyed;

    private Ship _ship;
    private bool _isActive;

    public void Initialize(Ship ship)
    {
        _ship = ship;
    }

    public void SetActive(bool isActive)
    {
        _meshRenderer.enabled = isActive;
        _meshCollider.enabled = isActive;
        _isActive = isActive;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
    }

    public void Tick(float deltaTime)
    {
        if (!_isActive)
        {
            return;
        }

        var rotationY = Quaternion.AngleAxis(_asteroidSettings.RotationSpeed * deltaTime, Vector3.up);
        transform.rotation *= rotationY;

        if (_ship != null)
        {
            transform.position += new Vector3(0, 0, -deltaTime * (_asteroidSettings.ForwardSpeed * _ship.ForwardSpeed));
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Catcher":
                Destroyed?.Invoke(this);
                break;
            case "Path":
                Destroyed?.Invoke(this);
                break;
            case "Asteroid":
                Destroyed?.Invoke(this);
                break;
            default:
                return;
        }
    }
}
