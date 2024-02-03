namespace _Project.Runtime.Data
{
    public enum GameEntityTag
    {
        Player = 0,
        Asteroid,
        AsteroidShard,
        Ufo,
        Laser,
        Bullet
    }
    
    public enum GameEnemy
    {
        AsteroidBig = 0,
        Asteroid = 1,
        AsteroidMedium = 2,
        AsteroidSmall = 3,
        AsteroidTiny = 4,
        Ufo = 5
    }

    public enum PlayerWeapon
    {
        BulletGun = 0,
        LaserGun = 1
    }
}