using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, Interactable
{
    public void OnInteraction(Player player)
    {
        var selectedObject = player.selectedObject;
        if (selectedObject != null && selectedObject.isType(typeof(Seed).Name))
        {
            var waterCan = (WaterCan)selectedObject;
            waterCan.fill();
        }
    }
}