using MFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class HomeModule : MainManager
    {

        public static void LoadModule()
        {
            SceneManager.LoadScene("Home");
        }
        protected override void LaunchInDevelopingMode()
        {
            Debug.Log("Developing mode");
            GameModule.LoadModule();
        }

        protected override void LaunchInProductionMode()
        {
            Debug.Log("Production mode");
            GameModule.LoadModule();
        }

        protected override void LaunchInTestMode()
        {
            Debug.Log("Test mode");
            GameModule.LoadModule();
        }
    }

}
