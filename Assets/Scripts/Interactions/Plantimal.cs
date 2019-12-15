using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantimal : MonoBehaviour, Interactable, Selectable
{
    
    [SerializeField]
    Attributes attribute;
    [SerializeField]
    string name;
    [SerializeField]
    float timeBeforeHungry = 10;
    [SerializeField]
    Sprite selectedSprite;
    [SerializeField]
    bool groundFriendly = true;

    bool isBeingHold = false;
    bool isFed = false;
    bool isGrown = false;

    Player player;
    Vector2 currentDirection;

    public void instanciate(Attributes attributes)
    {
        this.attribute = attributes;
    }

    Attributes getAttribute()
    {
        return attribute;
    }

    public void OnInteraction(Player player)
    {
        Debug.Log("I am " + name);
        var selectedObject = player.selectedObject;
        if (selectedObject != null)
        {
            if (selectedObject.isType(typeof(Insects).Name))
            {
                var insect = (Insects)selectedObject;
                feed(insect.getFood());
            }
                return;
        }
        player.setSelectedObject(this);
        this.player = player;
        selected();
    }

    public void selected()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        Debug.Log(name + ": You are holding me!");
        isBeingHold = true;
    }

    public void deSelected()
    {
        Debug.Log(name + ": You put me on the ground!");
        //Set it's position to playerPosition
        changeDirection(player.playerDirection);
        transform.position = player.transform.position + (Vector3)player.playerDirection;
        //Enable the game object
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isBeingHold = false;
        player = null;
    }

    public string getName()
    {
        return name;

    }

    public Sprite getSprite()
    {
        return selectedSprite;
    }

    public bool isGroundFriendly()
    {
        return groundFriendly;
    }

    public bool isType(string type)
    {
        return (this.GetType().Name == type);
    }

    public Attributes sendPlantimal()
    {
        Destroy(this.gameObject, 0.1f);
        return attribute;
    }

    private void feed(Food food)
    {
        attribute.food = food;
    }
    private IEnumerator waterCoolDown()
    {
        isFed = true;
        Debug.Log("Pot: I have water!");
        float cooldown = timeBeforeHungry;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Pot: I need water!");
        isFed = false;
    }

    private void changeDirection(Vector2 playerDirection)
    {
        currentDirection = playerDirection;
    }
}
