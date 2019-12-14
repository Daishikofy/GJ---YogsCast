using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request
{
    private int id;

    private string requestLetter;

    private int[] attributes;

    private int maxHapiness = 10;

    public Request(int id, string requestLetter, int[] attributes)
    {
        this.id = id;
        this.requestLetter = requestLetter;
        this.attributes = attributes;
    }
    public int getId()
    {
        return id;
    }
    public string getrequestLetter()
    {
        return requestLetter;
    }
    public int[] getAttributes()
    {
        return attributes;
    }

    public int getHapiness()
    {
        return maxHapiness;
    }
}
