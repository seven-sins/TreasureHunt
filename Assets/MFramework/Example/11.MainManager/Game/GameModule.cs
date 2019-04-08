using MFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameModule : MainManager
    {
        public static void LoadModule()
        {
            SceneManager.LoadScene("Game");
        }
        protected override void LaunchInDevelopingMode()
        {
            Debug.Log("Developing mode");
        }

        protected override void LaunchInProductionMode()
        {
            Debug.Log("Production mode");
        }

        protected override void LaunchInTestMode()
        {
            Debug.Log("Test mode");
        }
    }

}
