using UnityEngine;

public class SeedBag : MonoBehaviour, Interactable
{
    [SerializeField]
    private string objectType;
    [SerializeField]
    private Attributes.Animal seedType;
    [SerializeField]
    private Sprite objectSprite;
    [SerializeField]
    private GameObject plantPrefab;

    public string getType()
    {
        return this.objectType;
    }

    public void OnInteraction(Player player)
    {
        Debug.Log("Bag : This is a bag of " + objectType + " .");
        if (player.selectedObject == null)
        {
            var seed = new Seed(plantPrefab, objectType, objectSprite);
            seed.selected();
            player.setSelectedObject(seed);
        }
        else if (player.selectedObject.getName() == this.objectType)
        {
            player.selectedObject.deSelected();
            player.setSelectedObject(null);
        }
    }

}
