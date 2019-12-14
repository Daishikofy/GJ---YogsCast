using UnityEngine;

public class Seed : Selectable
{
    public string name;
    public Sprite selectedSprite;

    public Seed(string name, Sprite sprite)
    {
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

    public string getType()
    {
        return name;
    }

    public Sprite getSprite()
    {
        return selectedSprite;
    }
}
