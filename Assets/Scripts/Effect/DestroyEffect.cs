using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    public float delay;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
