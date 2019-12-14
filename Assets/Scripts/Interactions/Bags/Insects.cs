using UnityEngine;

public class Insects : Selectable
{
    public string name;
    public Sprite selectedSprite;

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

    public string getType()
    {
        return name;
    }

    public Sprite getSprite()
    {
        return selectedSprite;
    }
}
