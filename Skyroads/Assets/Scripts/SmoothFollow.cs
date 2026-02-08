using Settings;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField]
    private Ship _ship;

    [SerializeField]
    private CameraFollowSettings _settings;

    private void LateUpdate()
    {
        if (_ship == null)
            return;

        var shipTransform = _ship.transform;

        // Calculate the current rotation angles
        var wantedRotationAngle = shipTransform.eulerAngles.y;

        // Select distance and height
        var wantedHeight = shipTransform.position.y + (_ship.IsSpeedUp ? _settings.SpeedUpModeHeigh : _settings.Height) ;
        var wantedDistance = shipTransform.position.z + (_ship.IsSpeedUp ? -_settings.SpeedUpModeDistance : -_settings.Distance);

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;
        var currentDistance = transform.position.z;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _settings.DampingRotation * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _settings.DampingHeight * Time.deltaTime);
        // Damp the distance
        currentDistance = Mathf.Lerp(currentDistance, wantedDistance, _settings.DampingDistance * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane
        var pos = shipTransform.position - currentRotation * Vector3.forward;
        pos.z = currentDistance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(shipTransform);
    }
}