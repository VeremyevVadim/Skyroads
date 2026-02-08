using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ShipSettings", menuName = "Scriptable Objects/ShipSettings")]
    public class ShipSettings : ScriptableObject
    {
        public float HorizontalSpeed = 1f;

        public float ForwardSpeed = 1f;
        public float TurboSpeed = 2f;
        public float RotationPower = 3f;
    }
}