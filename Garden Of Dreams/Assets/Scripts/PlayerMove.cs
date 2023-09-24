using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public BagController Bag;

    [SerializeField] private float _speed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _visual, _gun;

    private float _dirX, _dirY;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _dirX = _joystick.Horizontal * _speed;
        _dirY = _joystick.Vertical * _speed;

        _rb.velocity = new Vector2(_dirX, _dirY);

        // Animation control
        if (_rb.velocity != new Vector2(0, 0))
        {
            GetComponent<Animator>().SetBool("IsMove", true);

            if (!GetComponent<PlayerShoot>().IsAutoShooting)
            {
                if (_dirX > 0)
                    GetComponent<Animator>().SetBool("IsMoveRight", true);
                if (_dirX < 0)
                    GetComponent<Animator>().SetBool("IsMoveRight", false);
            }
        }
        else
            GetComponent<Animator>().SetBool("IsMove", false);
    }
}
