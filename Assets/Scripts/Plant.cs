using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeHarvest;
    [SerializeField]
    private Sprite readyForHarvest;
    [SerializeField]
    private Animal animal;
    
    public float getTime()
    {
        return timeBeforeHarvest;
    }
    public void isReadyHarvest()
    {
        GetComponent<SpriteRenderer>().sprite = readyForHarvest;
    }

    public Animal getAnimal()
    {
        return animal;
    }
}
