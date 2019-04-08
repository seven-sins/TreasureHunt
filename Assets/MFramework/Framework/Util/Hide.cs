using UnityEngine;

namespace MFramework
{
    public class Hide : MonoBehaviourSimplify
    {
        protected override void OnBeforeDestroy()
        {
        }

        private void Awake()
        {
            this.Hide();
        }


    }

}
