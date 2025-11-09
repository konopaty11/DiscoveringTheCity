using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rg;
    [SerializeField] float _force;
    [SerializeField] float _speed;

    InputSystem_Actions _inputActions;
    Vector2 _input;

    void Awake()
    {
        _inputActions = new();
    }

    void Start()
    {
        StartCoroutine(MoveX());
    }

    void OnEnable()
    {
        _inputActions.Player.Move.Enable();
        _inputActions.Player.Move.performed += MoveXPerfomed;
    }

    void OnDisable()
    {
        _inputActions.Player.Giroscope.Disable();
        _inputActions.Player.Giroscope.performed -= MoveXPerfomed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
            _rg.AddForceY(_force);
    }

    IEnumerator MoveX()
    {
        float _x = transform.position.x;
        while (true)
        {
            _x = Mathf.Lerp(_x, _x + _input.x, Time.deltaTime * _speed);
            transform.position = new(_x, transform.position.y, transform.position.z);

            yield return null;
        }
    }

    void MoveXPerfomed(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }
}
