using Asteroids.Runtime.Weapons.Data;
using UnityEngine;

namespace Asteroids.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Create/PlayerConfig", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Movement")]
        public float rotateAngle = 5f;
        public float rotateSpeed;

        public float maxSpeed = 1;
        public float accelerationSpeed = 1f;
        [Range(0f, 1f)] public float drag;

        [Header("Weapon Settings")] 
        public WeaponData[] weapons;
        
    }
}