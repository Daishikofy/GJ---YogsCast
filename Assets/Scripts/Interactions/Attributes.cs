using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {
    [System.Serializable]
    public enum Animal
    {
        Chick,
        Animal2,
        Animal3
    };

    [System.Serializable]
    public enum Biome
    {
        Hayfield,
        Desert,
        Island,
        SnowyMountain
    };

    [System.Serializable]
    public enum Food
    {
        normal,
        Red,
        Blue,
        Purple
    };
}

