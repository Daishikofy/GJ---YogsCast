using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestGenerator : MonoBehaviour
{
    [SerializeField]
    private string[] attribute1;
    [SerializeField]
    private string[] attribute2;
    [SerializeField]
    private string[] attribute3;
    public void generateNewRequest()
    {
        int randIndex = Random.Range(0, attribute1.Length);
        string request = "I would like, " + attribute1[randIndex];
        randIndex = Random.Range(0, attribute2.Length);
        request += " " + attribute2[randIndex];
        randIndex = Random.Range(0, attribute3.Length);
        request += " " + attribute3[randIndex] + ".";
        Debug.Log(request);
        //return request;
    }
}
