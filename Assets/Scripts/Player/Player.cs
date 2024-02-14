using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] public int speed = 10;
    [SerializeField] public Sprite frontView;
    [SerializeField] public Sprite backView;
    [SerializeField] public Sprite rightView;
    [SerializeField] public LayerMask mineable;

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
        input.Player.Move.performed += Moving;
        input.Player.Move.canceled += Stopping;
        input.Player.Mouse.performed += Interact;
    }

    void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= Moving;
        input.Player.Move.canceled -= Stopping;
        input.Player.Mouse.performed -= Interact;
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
            playerSprite.flipX = false;
            playerSprite.sprite = rightView;
        }
        else if (moveVector.x < 0)
        {
            playerSprite.flipX = true;

            if (playerSprite.sprite != rightView)
            {
                playerSprite.sprite = rightView;
            }
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

    private void Interact(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, Vector2.right, 1.0f, mineable
            );

            if (hit.collider != null)
            {
                Debug.Log($"Hit {hit.collider.name}");
                StartCoroutine(hit.collider.GetComponent<Mineable>().Mining());
            }
        }
    }
}
