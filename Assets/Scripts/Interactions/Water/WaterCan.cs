using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour, Selectable, Interactable
{
    [SerializeField]
    string name;
    [SerializeField]
    int maxCapacity;
    [SerializeField]
    Sprite selectedSprite;
    [SerializeField]
    bool groundFriendly = true;

    bool isBeingHold = false;
    int currentWater;
    Player player;

    EventInt updateWater;

    private void Start()
    {
        setWater(maxCapacity);
    }
    public void OnInteraction(Player player)
    {
        Debug.Log("I am " + name);
        if (player.selectedObject != null) return;
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
        transform.position = player.transform.position + (Vector3)player.playerDirection;
        //Enable the game object
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isBeingHold = false;
        player = null;
    }

    public Sprite getSprite()
    {
        return selectedSprite;
    }

    public string getName()
    {
        return name;
    }

    public bool isGroundFriendly()
    {
        return groundFriendly;
    }

    public bool isType(string type)
    {
        return (this.GetType().Name == type);
    }

    public void use(Pots pot)
    {
        if (currentWater <= 0)
        {
            Debug.Log("Water Can : There is no more water!");
            return;
        }
        currentWater -= 1;
        pot.setWater(true);
    }

    public void fill()
    {
        Debug.Log("Water Can : I am full!!");
        setWater(maxCapacity);
    }

    private void setWater(int value)
    {
        currentWater = value;
    }
}
