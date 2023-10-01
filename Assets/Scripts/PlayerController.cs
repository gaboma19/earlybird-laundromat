using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public PlayerInputActions playerControls;
    private InputAction move;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();

        horizontal = moveDirection.x;
        vertical = moveDirection.y;

        if (!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

}
