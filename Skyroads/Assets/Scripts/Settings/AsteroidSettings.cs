using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "AsteroidSettings", menuName = "Scriptable Objects/AsteroidSettings")]
    public class AsteroidSettings : ScriptableObject
    {
        [Tooltip("Degrees per second")]
        public float RotationSpeed = 180;

        public float ForwardSpeed = 10f;
    }
}
