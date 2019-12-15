using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantimal : MonoBehaviour, Interactable, Selectable
{
    [SerializeField]
    int[] attribute = new int[3];
    [SerializeField]
    string name;
    [SerializeField]
    Sprite selectedSprite;
    [SerializeField]
    bool groundFriendly = true;

    bool isBeingHold = false;

    Player player;

    public void instance(int[] attribute)
    {
        this.attribute = attribute;
    }

    int[] getAttribute()
    {
        return attribute;
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

    public int[] sendPlantimal()
    {
        Destroy(this.gameObject, 0.1f);
        return attribute;
    }
}
