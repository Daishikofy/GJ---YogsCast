using UnityEngine;

public class Seed : Selectable
{
    public string name = "( Seed : I am deprecated :D )";
    public Sprite selectedSprite;
    private Attributes.Animal seedType;
    [SerializeField]
    bool groundFriendly = false;

    [SerializeField]
    GameObject plantPrefab;

    public Seed(Attributes.Animal type, string name,  Sprite sprite)
    {
        this.seedType = type;
        this.name = name;
        this.selectedSprite = sprite;
    }

    public void selected()
    {
        Debug.Log("You selected a " + name + " seed.");
    }

    public void deSelected()
    {
        Debug.Log("You put the " + name + " seed back in the pot.");
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

    public GameObject getPlantPrefab()
    {
        return plantPrefab;
    }
}
