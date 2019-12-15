using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantimalFactory : MonoBehaviour
{
    private static PlantimalFactory instance;
    [SerializeField]
    GameObject plantimal;

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
        var newPlantimal = Instantiate(plantimal, Vector3.zero, Quaternion.identity);
        var attributes = new Attributes(animal, biome, Food.normal);       
        plantimal.GetComponent<Plantimal>().instanciate(attributes);
        //newPlantimal.GetComponent<SwapSprites>().SpriteSheetName = getFirstPlantimal(animal);
        return plantimal;
    }
}
