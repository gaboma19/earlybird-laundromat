using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes
{
    public enum TYPE
    {
        SHIRT, PANTS, UNDERWEAR, SOCKS
    }

    public enum COLOR
    {
        DARK, LIGHT
    }
    TYPE type { get; }
    COLOR color { get; }

    public Clothes()
    {
        type = GetRandomEnum<TYPE>();
        color = GetRandomEnum<COLOR>();
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public Clothes(TYPE _type, COLOR _color)
    {
        type = _type;
        color = _color;
    }

}

