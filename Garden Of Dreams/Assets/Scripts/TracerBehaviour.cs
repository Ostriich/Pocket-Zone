using UnityEngine;

public class TracerBehaviour : MonoBehaviour
{
    public Vector2 Direction;

    [SerializeField] private float _speed;
    private float _wait;

    //Tracer flying when player fired
    private void FixedUpdate()
    {
        transform.Translate(Direction.normalized * _speed / 2);

        _wait += Time.deltaTime;
        if (_wait >= 0.2)
            Destroy(gameObject);
    }
}
