using _Project.Runtime.Controllers.Base;

namespace _Project.Runtime.Controllers.Saves
{
    public interface IProgressController:IController
    {
        int HighScore { get; }
        
        int CurrentScore { get; set; }
    }
}