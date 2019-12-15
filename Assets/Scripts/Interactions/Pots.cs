using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pots : MonoBehaviour, Interactable
{
    [SerializeField]
    Biome biome;
    [SerializeField]
    float waterGrowthBoost = 1;
    [SerializeField]
    float wateredTime = 10;

    float timeBeforeHarvest;
    float timeSincePlanted;

    bool isOccupied = false;
    bool isWatered = false;
    bool isReadyForHarvest = false;
    Plant plant;

    public void OnInteraction(Player player)
    {
        var selectedObject = player.selectedObject;
        if (selectedObject != null)
        {
            Debug.Log("Pot : You are holding something");
            if (selectedObject.isType(typeof(Seed).Name))//Planting
            {
                player.selectedObject.deSelected();
                player.setSelectedObject(null);
                Seed seed = (Seed)selectedObject;
                plant = Instantiate(seed.getPlantPrefab(), this.transform.position, Quaternion.identity, this.transform).GetComponent<Plant>();
                putPlantInPot();
                Debug.Log("Pot: You just planted a " + selectedObject.getName());
            }
            else if (selectedObject.isType(typeof(WaterCan).Name) && !isWatered)
            {
                Debug.Log("Pot: I like water");//Watering
                WaterCan waterCan = (WaterCan)selectedObject;
                waterCan.use(this);       
            }
        }
        else if (selectedObject == null && isReadyForHarvest)
        {
            Debug.Log("Pot: It is harvest time!");//Harvesting
            harvest(player);
            //If the plant is ready, harvest
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isOccupied || isReadyForHarvest) return;
        if (timeSincePlanted >= timeBeforeHarvest)
        {
            setReadyForHarvest (true);
            return;
        }
        if (isWatered)
            timeSincePlanted += Time.deltaTime * waterGrowthBoost;
        else
            timeSincePlanted += Time.deltaTime;
    }

    private void putPlantInPot()
    {
        timeBeforeHarvest = plant.getTime();
        isOccupied = true;
    }

    private void resetPot()
    {
        isOccupied = false;
        setReadyForHarvest(false);
        Destroy(plant.gameObject);
        setWater(false);
    }

    private void setReadyForHarvest(bool value)
    {
        if (value)
        {
            Debug.Log("Pot : Ready for harvest!!");
            isReadyForHarvest = true;
            plant.isReadyHarvest();
        }
        else
        {
            timeSincePlanted = 0f;
            isReadyForHarvest = false;
        }
    }

    private void harvest(Player player)
    {
        var plantanimal = PlantimalFactory.Instance.instanciatePlantimal(plant.getAnimal(), biome);
        Plantimal plantimal = plantanimal.GetComponent<Plantimal>();
        Debug.Log("NAME2: " + plantimal.getName());
        plantimal.OnInteraction(player);
        Debug.Log("NAME3: " + plantimal.getName());

        resetPot();
    }

    public void setWater(bool value)
    {
        isWatered = value;
        if (value)          
            StartCoroutine(waterCoolDown());     
        else          
            StopCoroutine(waterCoolDown());
        
    }
    private IEnumerator waterCoolDown()
    {
        isWatered = true;
        Debug.Log("Pot: I have water!");
        float cooldown = wateredTime;
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Pot: I need water!");
        isWatered = false;    
    }
}
