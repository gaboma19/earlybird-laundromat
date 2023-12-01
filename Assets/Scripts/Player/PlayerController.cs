using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Rigidbody2D rigidbody2d;
    Animator animator;
    BoxCollider2D boxCollider2D;
    Vector2 lookDirection = new(0, -1);
    Vector2 moveDirection;
    public PlayerInputActions playerControls;
    private InputAction move;

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        playerControls = new PlayerInputActions();
        Minigame.OnMinigameStarted += DisableMove;
        Minigame.OnMinigameEnded += (_) => EnableMove();
        Minigame.OnMinigameKilled += EnableMove;
        Origami.OnOrigamiStarted += DisableMove;
        Origami.OnOrigamiEnded += (_) => EnableMove();
        Origami.OnOrigamiKilled += EnableMove;
        DialogueBoxController.OnDialogueStarted += DisableMove;
        DialogueBoxController.OnDialogueEnded += EnableMove;
        Apartment.OnDayEnded += DisableMove;
        Apartment.OnDayStarted += EnableMove;
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

    private void DisableMove()
    {
        move.Disable();
        boxCollider2D.enabled = false;
    }

    private void EnableMove()
    {
        move.Enable();
        boxCollider2D.enabled = true;
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

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
        position.x += speed * moveDirection.x * Time.deltaTime;
        position.y += speed * moveDirection.y * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

}
