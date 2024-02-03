using Asteroids.Runtime;
using Asteroids.Runtime.Weapons.Data;
using Asteroids.Runtime.UI;
using Asteroids.Runtime.Views;
using UnityEngine;

namespace Asteroids.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Create/GameConfig")]
    public class GamePrefabs : ScriptableObject
    {
        [Header("UI")] public MainUI MainUIPrefab;
        public GameOverPopup GameOverPopupPrefab;

        [Header("Player")] public PlayerView PlayerViewPrefab;

        [Header("Weapons")] public WeaponPrefabs WeaponPrefabs;

        [Header("Enemies")] public EnemiesPrefabs EnemiesPrefabs;
    }
}