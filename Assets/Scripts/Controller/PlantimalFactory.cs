using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantimalFactory : MonoBehaviour
{
    private static PlantimalFactory instance;
    [SerializeField]
    GameObject plantimal;
    [SerializeField]
    string[] names;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public static PlantimalFactory Instance { get { return instance; } }

    public string getFirstPlantimal(Animal value)
    {
        return value.ToString();
    }

    public string getSecondPlantimal(Attributes attributes)
    { 
        return attributes.ToString();
    }

    public GameObject instanciatePlantimal(Animal animal, Biome biome)
    {
        Debug.Log("INSTANCIATE A PLANTIMAL");
        

        var newPlantimal = Instantiate(plantimal, Vector3.zero, Quaternion.identity);

        string name = names[Random.Range(0, names.Length)];
        var attributes = new Attributes(animal, biome, Food.Normal);
        SwapSprites swapSprites = newPlantimal.GetComponent<SwapSprites>();
        swapSprites.SpriteSheetName = getFirstPlantimal(animal);
        var sprite = swapSprites.getFirstSprite();
        newPlantimal.GetComponent<Plantimal>().instanciate(attributes, sprite, name);
        swapSprites.SpriteSheetName = animal.ToString();

        return newPlantimal;
    }
}
