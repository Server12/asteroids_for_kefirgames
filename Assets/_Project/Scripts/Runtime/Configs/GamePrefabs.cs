using _Project.Runtime.Controllers;
using _Project.Runtime.Controllers.Weapons.Data;
using _Project.Runtime.UI;
using _Project.Runtime.Views;
using UnityEngine;

namespace _Project.Scripts.Runtime.Data
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