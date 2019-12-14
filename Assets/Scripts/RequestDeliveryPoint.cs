using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDeliveryPoint : MonoBehaviour, Interactable
{
    public void OnInteraction(Player player)
    {
        string type = typeof(Plantimal).Name;
        var selectedObject = player.selectedObject;
        if (player.selectedObject != null && player.selectedObject.isType(type))
        {
            Debug.Log("This is a plantimal, let it here");
            bool res = RequestManager.Instance.validateRequest((Plantimal)selectedObject);
            if (!res)
                return;
            player.setSelectedObject(null);
        }
        else
            Debug.Log("You give the plantimals here");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
