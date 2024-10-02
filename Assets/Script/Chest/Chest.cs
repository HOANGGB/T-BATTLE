using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chest", menuName = "ScriptableObjects/Chest",order = 3)]

public class Chest : ScriptableObject
{
    public string Name;
    public Sprite Img;
    public int Gold;
    public int Gem;

}
