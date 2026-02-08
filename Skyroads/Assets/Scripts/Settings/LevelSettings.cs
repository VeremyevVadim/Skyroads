using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable Objects/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [Tooltip("Spawn targets in min")]
        [Range(0, 1000)]
        public float SpawnFrequency = 100;

        public float RoadWidth = 8f;
        public float FlyHeight = 1.5f;

        public int PointsGainPerSecond = 1;
        public int PointsGainPerAsteroid = 5;
        public int AsteroidsToLvlUp = 20;
        public int FrequancyPerLvlUp = 20;
    }
}