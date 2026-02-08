using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Scriptable Objects/CameraSettings")]
    public class CameraFollowSettings : ScriptableObject
    {
        public float Distance = 10.0f;
        public float Height = 5.0f;

        public float SpeedUpModeDistance = 5.0f;
        public float SpeedUpModeHeigh = 2.5f;

        public float DampingHeight = 2.0f;
        public float DampingDistance = 2.0f;
        public float DampingRotation = 3.0f;
    }
}
