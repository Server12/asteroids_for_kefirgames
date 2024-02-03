using Asteroids.Runtime.Base;

namespace Asteroids.Runtime.Saves
{
    public interface IProgressController:IController
    {
        int HighScore { get; }
        
        int CurrentScore { get; set; }
    }
}