using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    private Vector2 playerMovement;
    [HideInInspector]
    public Vector2 playerDirection;
    bool isMoving = false;
    Animator animator;

    public Selectable selectedObject;
    [SerializeField]
    private SpriteRenderer selectedObjectSprite;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.X.performed += context => horizontal(context.ReadValue<float>());
        playerInput.Player.Y.performed += context => vertical(context.ReadValue<float>());
        playerInput.Player.Interact.performed += _ => interacting();
        rigidbody2D = GetComponent<Rigidbody2D>();

        selectedObjectSprite.sprite = null;

        playerDirection = Vector2.down;
        animator.SetFloat("X", playerDirection.x);
        animator.SetFloat("Y", playerDirection.y);
    }

    private void Update()
    {
        if (playerMovement == Vector2.zero)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }

    private void FixedUpdate()
    {       
        rigidbody2D.MovePosition(rigidbody2D.position + playerMovement * Time.deltaTime);
    }

    private void horizontal(float value)
    {
 //       Debug.Log("horizontal:" + value);
        if (value == 0 && playerMovement.y != 0)
            return;
        else
        {
            this.playerMovement.x = (int)value;
            this.playerMovement.y = 0;
        }
        if (playerMovement.x != 0 || playerMovement.y != 0)
            playerDirection = playerMovement;
        playerMovement *= speed;
        animator.SetFloat("X", playerDirection.x);
        animator.SetFloat("Y", playerDirection.y);
    }

    private void vertical(float value)
    {
//        Debug.Log("vertical: " +value);
        if (value == 0 && playerMovement.x != 0)
            return;
        else
        {
            this.playerMovement.x = 0;
            this.playerMovement.y = (int)value;
        }
        if(playerMovement.x != 0 || playerMovement.y != 0)
            playerDirection = playerMovement;
        playerMovement *= speed;
        animator.SetFloat("X", playerDirection.x);
        animator.SetFloat("Y", playerDirection.y);
    }

    private void interacting()
    {
        Vector3 startPoint = transform.position + Vector3.up * 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, playerDirection, 0.5f);
        Debug.Log("fraction: " + hit.fraction);
        Debug.DrawRay(startPoint, playerDirection, Color.red, 1);

        if (hit.collider == null)
        {
            if (selectedObject != null && selectedObject.isGroundFriendly())
            {
                selectedObject.deSelected();
                setSelectedObject(null);
            }
            return;
        }
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
        //Debug.Log(hit.collider.gameObject.name);

        if (interactable == null) return;
        interactable.OnInteraction(this);
    }

    public void setSelectedObject(Selectable obj)
    {
        if (obj == null)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            selectedObjectSprite.sprite = null;
            Debug.Log("Player: You put down the " + selectedObject.getName());
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            selectedObjectSprite.sprite = obj.getSprite();
            Debug.Log("Player: You pick the " + obj.getName());
        }

        selectedObject = obj;
    }
}
