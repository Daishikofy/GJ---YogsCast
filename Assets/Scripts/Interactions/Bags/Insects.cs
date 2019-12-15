using UnityEngine;

public class Insects : Selectable
{
    public string name;
    public Sprite selectedSprite;
    [SerializeField]
    Food food;
    [SerializeField]
    bool groundFriendly = false;

    public Insects(string name, Sprite sprite)
    {
        this.name = name;
        this.selectedSprite = sprite;
    }

    public void selected()
    {
        Debug.Log("You selected a " + name + ".");
    }

    public void deSelected()
    {
        Debug.Log("You put the " + name + " seed back in the bag.");
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

    public Food getFood()
    {
        return food;
    }
}
