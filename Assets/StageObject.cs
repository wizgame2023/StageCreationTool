using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    [SerializeField] private int objectNumber;
    
    public int GetObjcectNumber()
    {
        return objectNumber;
    }   
}
