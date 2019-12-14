using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBag : MonoBehaviour, Interactable
{
    [SerializeField]
    private string seedType;
    [SerializeField]
    private Sprite seedSprite;

    public string getType()
    {
        return this.seedType;
    }

    public void OnInteraction(Player player)
    {
        Debug.Log("Bag of seed: This is a bag of " + seedType + " seeds.");
        if (player.selectedObject == null)
        {
            var seed = new Seed(seedType, seedSprite);
            seed.selected();
            player.setSelectedObject(seed);
        }
        else if (player.selectedObject.getType() == this.seedType)
        {
            player.selectedObject.deSelected();
            player.setSelectedObject(null);
        }
    }


}
