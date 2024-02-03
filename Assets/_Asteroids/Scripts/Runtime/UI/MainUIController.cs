using System;
using Asteroids.Runtime;
using Asteroids.Runtime.Weapons.Models;
using Asteroids.Runtime.Data;
using Object = UnityEngine.Object;

namespace Asteroids.Runtime.UI
{
    public class MainUIController : IMainUIController, IUpdate, IInitializable
    {
        public event Action OnStartPlayGame;


        private readonly MainUI _mainUIPrefab;
        private IPlayerController _playerController;

        private MainUI _mainUI;

        private IPlayerController _player;
        private IWeaponModel _laserWeaponModel;

        public MainUIController(MainUI mainUIPrefab)
        {
            _mainUIPrefab = mainUIPrefab;
        }

        public void Initialize()
        {
            _player = GameController.GetController<IPlayerController>();

            _laserWeaponModel = _player.WeaponsManager.GetWeaponModel(PlayerWeapon.LaserGun);

            _mainUI = Object.Instantiate(_mainUIPrefab);
            _mainUI.coordsText.text = "";
            _mainUI.laserBullets.text = "";
            _mainUI.laserTime.text = "";
            _mainUI.rotationText.text = "";
            _mainUI.velocityText.text = "";
            _mainUI.playButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            _mainUI.playButton.onClick.RemoveListener(OnPlayClicked);
            _mainUI.playButton.gameObject.SetActive(false);
            OnStartPlayGame?.Invoke();
        }
        

        public IGameController GameController { private get; set; }

        public void Update(float deltaTime)
        {
            _mainUI.coordsText.text = $"x:{_player.Position.x:f0} y:{_player.Position.y:f0}";
            _mainUI.rotationText.text = $"rotation:{_player.CurrentRotation:f0}";
            _mainUI.velocityText.text = $"velocity:{_player.Velocity.magnitude:f0}";
            _mainUI.laserBullets.text = $"Laser Count:{_laserWeaponModel.BulletsCounter}";
            _mainUI.laserTime.text = $"Laser time:{_laserWeaponModel.BulletsChargeTimer:f0}";
        }
    }
}