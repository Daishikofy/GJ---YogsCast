using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantimalFactory : MonoBehaviour
{
    private static PlantimalFactory instance;
    [SerializeField]
    GameObject[] firstPlatimals;
    [SerializeField]
    GameObject[][] finalPlatimals1;
    [SerializeField]
    GameObject[][] finalPlatimals2;
    [SerializeField]
    GameObject[][] finalPlatimals3;

    GameObject[][][] platimals;
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

    private void Start()
    {
        platimals = new GameObject[3][][];
        platimals[0] = finalPlatimals1;
        platimals[1] = finalPlatimals2;
        platimals[2] = finalPlatimals3;
    }
    public static PlantimalFactory Instance { get { return instance; } }

    public GameObject getFirstPlantimal(Attributes.Animal value)
    {
        return firstPlatimals[(int)value];
    }

    public GameObject getSecondPlantimal(int[] value)
    {
        return platimals[value[0]][value[1]][value[2]];
    }
}
