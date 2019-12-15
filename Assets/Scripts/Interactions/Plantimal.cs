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
    int feedTimesToGrow = 3;
    [SerializeField]
    Sprite selectedSprite;
    [SerializeField]
    bool groundFriendly = true;

    int fedTimes;
    bool isColored;
    Food currentFood;

    bool isBeingHold = false;
    bool isFed = false;
    public bool isGrown = false;

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
        Debug.Log("Attributes: " + attribute.ToString());
        var selectedObject = player.selectedObject;
        if (selectedObject != null)
        {
            if (selectedObject.isType(typeof(Insects).Name))
            {
                if (isFed || isGrown)
                {
                    //Sound : Error
                    return;
                }
                var insect = (Insects)selectedObject;
                feed(insect.getFood());
                selectedObject.deSelected();
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
        if (fedTimes > 0)
        {
            if (currentFood != food)
                isColored = false;
        }
        currentFood = food;
        fedTimes += 1;
        StartCoroutine(foodCoolDown());
    }
    private IEnumerator foodCoolDown()
    {
        setFed(true);
        float cooldown = timeBeforeHungry;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        setFed (false);
        if (fedTimes >= feedTimesToGrow)
            grow();
    }

    private void setFed(bool value)
    {
        isFed = value;
        if (isFed)
            Debug.Log(name + " : I am not hungry anymore!");
        else
            Debug.Log(name + " : I am sooo hungry now!");
        //TODO: Insert hungry sprite
    }

    private void grow()
    {
        isGrown = true;
        Debug.Log(name + " : I am a grown up now!");
    }

    private void changeDirection(Vector2 playerDirection)
    {
        currentDirection = playerDirection;
    }
}
