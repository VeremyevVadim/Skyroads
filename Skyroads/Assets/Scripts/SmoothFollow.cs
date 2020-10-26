using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    private float distance = 10.0f;
    [SerializeField]
    private float height = 5.0f;

    [SerializeField]
    private float distanceSpeedUpMode = 5.0f;
    [SerializeField]
    private float heighSpeedUpModet = 2.5f;

    [SerializeField]
    private float heightDamping = 2.0f;
    [SerializeField]
    private float distanceDamping = 2.0f;
    [SerializeField]
    private float rotationDamping = 3.0f;

    [SerializeField]
    private Ship Ship = null;

    private Transform _target;

    private void Start()
    {
        if (!Ship)
        {
            return;
        }

        _target = Ship.transform;
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = _target.eulerAngles.y;

        // Select distance and height
        float wantedHeight = _target.position.y + (Ship.IsSpeedUp ? heighSpeedUpModet : height) ;
        float wantedDistance = _target.position.z + (Ship.IsSpeedUp ? -distanceSpeedUpMode : -distance);

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
        float currentDistance = transform.position.z;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        // Damp the distance
        currentDistance = Mathf.Lerp(currentDistance, wantedDistance, distanceDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane
        var pos = _target.position - currentRotation * Vector3.forward;
        pos.z = currentDistance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
    }
}