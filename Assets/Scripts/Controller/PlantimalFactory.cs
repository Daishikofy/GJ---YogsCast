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

    public string getFirstPlantimal(Attributes.Animal value)
    {
        return value.ToString();
    }

    public string getSecondPlantimal(Attributes.Animal animal, Attributes.Biome biome,Attributes.Food food)
    {
        return animal.ToString()+biome.ToString()+food.ToString();
    }

    public GameObject instanciatePlantimal(Attributes.Animal animal)
    {
        var newPlantimal = Instantiate(plantimal, Vector3.zero, Quaternion.identity);
        //newPlantimal.GetComponent<SwapSprites>().SpriteSheetName = getFirstPlantimal(animal);
        return plantimal;
    }
}
