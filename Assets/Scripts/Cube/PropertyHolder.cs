using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyHolder : MonoBehaviour
{
    public static PropertyHolder instance;
    public CubeStackProperty[] cubeStackProperties;

    public void Awake()
    {
        instance = this;
    }

    public CubeStackProperty GetProperty(int value)
    {
        CubeStackProperty property = null;
        foreach(var item in cubeStackProperties)
        {
            if (item.Value == value)
                property = item;
        }
        return property;
    }

    public CubeStackProperty GetRandomPropertyTo64()
    {
        return cubeStackProperties[Random.Range(0, 5)];
    }
}
