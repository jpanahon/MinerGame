using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] public int speed = 10;
    [SerializeField] public Sprite frontView;
    [SerializeField] public Sprite backView;
    [SerializeField] public Sprite leftView;
    [SerializeField] public Sprite rightView;

    private SpriteRenderer playerSprite = null;

    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;

    private Rigidbody2D rigidBody = null;

    void Awake()
    {
        input = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        input.Enable();
        input.Movement.Move.performed += Moving;
        input.Movement.Move.canceled += Stopping;
    }

    void OnDisable()
    {
        input.Disable();
        input.Movement.Move.performed -= Moving;
        input.Movement.Move.canceled -= Stopping;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = moveVector * speed;
    }
    private void Moving(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();

        if (moveVector.x > 0)
        {
            playerSprite.sprite = rightView;
        }
        else if (moveVector.x < 0)
        {
            playerSprite.sprite = leftView;
        }

        if (moveVector.y > 0)
        {
            playerSprite.sprite = backView;
        }
        else if (moveVector.y < 0)
        {
            playerSprite.sprite = frontView;
        }
    }

    private void Stopping(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
