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
    private Attributes.Animal animal;
    
    public float getTime()
    {
        return timeBeforeHarvest;
    }
    public void isReadyHarvest()
    {
        GetComponent<SpriteRenderer>().sprite = readyForHarvest;
    }

    public Attributes.Animal getAnimal()
    {
        return animal;
    }
}
