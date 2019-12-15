using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantimal : MonoBehaviour, Interactable, Selectable
{
    
    [SerializeField]
    Attributes attribute;
    [SerializeField]
    string myName;
    [SerializeField]
    float timeBeforeHungry = 10;
    [SerializeField]
    int feedTimesToGrow = 3;

    public Sprite selectedSprite;
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

    public void instanciate(Attributes attributes, Sprite sprite, string name)
    {
        Debug.Log(name + " Is born");
        this.myName = name;
        this.attribute = attributes;
        this.selectedSprite = sprite;
    }

    Attributes getAttribute()
    {
        return attribute;
    }

    public void OnInteraction(Player player)
    {
        Debug.Log("I am " + myName);
        Debug.Log("Attributes: " + attribute.ToString());

        if (selectedSprite == null)
            setFirstSprite();
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
        
        //Debug.Log(name + ": You are holding me!");
        isBeingHold = true;
    }

    public void deSelected()
    {
        transform.position = player.transform.position + (Vector3)player.playerDirection;
 
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        changeDirection(player.playerDirection);
        isBeingHold = false;
        player = null;
    }

    public string getName()
    {
        return myName;
    }
    public void setName(string name)
    {
        this.myName = name;
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
            Debug.Log(myName + " : I am not hungry anymore!");
        else
            Debug.Log(myName + " : I am sooo hungry now!");
        //TODO: Insert hungry sprite
    }

    private void grow()
    {
        isGrown = true;
        Debug.Log(myName + " : I am a grown up now!");
    }

    private void changeDirection(Vector2 playerDirection)
    {
        currentDirection = playerDirection;
        
        GetComponent<Animator>().SetFloat("X",currentDirection.x);
        GetComponent<Animator>().SetFloat("Y", currentDirection.y);
    }

    private void setFirstSprite()
    {
        SwapSprites swap = GetComponent<SwapSprites>();
        selectedSprite = swap.getFirstSprite();
    }
}
