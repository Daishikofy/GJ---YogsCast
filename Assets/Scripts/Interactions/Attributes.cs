using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

[System.Serializable]
public class Attributes
{
    public Animal animal;
    public Biome biome;
    public Food food;

    public Attributes(Animal animal, Biome biome, Food food)
    {
        this.animal = animal;
        this.biome = biome;
        this.food = food;
    }
    public override string ToString()
    {
        return animal.ToString() + biome.ToString() + food.ToString();
    }

    public int[] ToInt()
    {
        var array = new int[] { (int)animal, (int)biome, (int)food};
        return array;
    }
}


