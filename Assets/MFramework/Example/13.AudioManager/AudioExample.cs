using UnityEngine;

namespace MFramework
{
    public class AudioExample : MonoBehaviourSimplify
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/13.AudioManager", false, 13)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("AudioExample").AddComponent<AudioExample>();
        }
#endif
        protected override void OnBeforeDestroy()
        {
        }

        private void Start()
        {
            // AudioManager.Instance.PlaySound("yu");
            AudioManager.Instance.PlayMusic("yu", true);

            Delay(1.0f, () => AudioManager.Instance.PauseMusic());

            Delay(3.0f, () => AudioManager.Instance.ResumeMusic());

            Delay(5.0f, () => AudioManager.Instance.StopMusic());
        }
    }

}
