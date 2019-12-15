using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour, Interactable
{
    [SerializeField]
    Attributes.Biome biome;
    [SerializeField]
    float waterGrowthBoost;
    [SerializeField]
    float wateredTime;

    float timeSinceWater;
    bool isOccupied;
    bool isWatered;
    GameObject plant;
    public void OnInteraction(Player player)
    {
        var selectedObject = player.selectedObject;
        if (selectedObject != null)
        {
            if (selectedObject.isType(typeof(Seed).Name))
                Debug.Log("You just planted a " + selectedObject.getType());
            if (selectedObject.isType(typeof(WaterCan).Name))
                Debug.Log("You just planted a " + selectedObject.getType());
        }
        else if (selectedObject == null)
        {
            //If the plant is ready, harvest
        }
    }
    // Update is called once per frame
    void Update()
    {
            if (!isOccupied) return;
    }

    private void resetPot()
    {
        plant = null;
        isWatered = false;
    }

    private void setWater(bool value)
    {
        if (value)
        {
            isWatered = value;

        }
    }
    private IEnumerator waterCoolDown()
    {
        isWatered = true;
        float cooldown = wateredTime;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isWatered = false;    
    }
}
