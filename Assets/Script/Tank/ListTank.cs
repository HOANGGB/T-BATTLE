using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTank : MonoBehaviour
{
    [HideInInspector] public static ListTank instance;
    public List<Tank> AllTank = new List<Tank>();
    void Awake()
    {
        instance = this;
    }
}
