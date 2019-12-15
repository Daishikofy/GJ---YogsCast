using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dashFactor;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCoolDown;

    private IEnumerator dashCorroutine;
    private bool canDash;
    private bool dashing;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    private Vector2 playerMovement;
    [HideInInspector]
    public Vector2 playerDirection;

    public Selectable selectedObject;
    [SerializeField]
    private SpriteRenderer selectedObjectSprite;

    // Use this for initialization
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Player.X.performed += context => horizontal(context.ReadValue<float>());
        playerInput.Player.Y.performed += context => vertical(context.ReadValue<float>());
        playerInput.Player.Interact.performed += _ => interacting();
        rigidbody2D = GetComponent<Rigidbody2D>();

        canDash = true;
        dashing = false;
        selectedObjectSprite.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * PlayerInput receives values between -1 and 1 and is then normalized
         * PlayerMovement representes those values multiplied by the player's speed
         * The speed is only applied after going throught the dash test.
         */

        //playerMovement = Vector2.zero;
        //We use corroutine for the dash so the update function stays clean
        if (/*Input.GetKeyDown(KeyCode.Space) && canDash*/ 1 == 0)
        {
            canDash = false;
            dashing = true;
            dashCorroutine = Dash();
            StartCoroutine(dashCorroutine);
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("rigidbody2D.position" + rigidbody2D.position + " playerMovement " + playerMovement);
        rigidbody2D.MovePosition(rigidbody2D.position + playerMovement * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        /*
         * The speed is multiply by the dash 
         * factor during the dashDuration
         */
        speed = speed * dashFactor;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += 1 / dashDuration;
            yield return null;
        }

        // It is then set to the original speed

        speed = speed / dashFactor;
        dashing = false;

        // then the coolDown happens, the player can only dash again after it.
        normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += 1 / dashCoolDown;
            yield return null;
        }

        canDash = true;
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
