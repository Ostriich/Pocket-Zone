using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    private float _wait;

    // Quick flash from shot
    private void FixedUpdate()
    {
        _wait += Time.deltaTime;
        if (_wait >= 0.1)
            Destroy(gameObject);
    }
}
