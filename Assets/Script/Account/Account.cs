using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Account", menuName = "ScriptableObjects/Account",order = 2)]

public class Account : ScriptableObject
{
    public string Name;
    public Sprite AvataImg;

    public int CupNumber;
    public int Gold;
    public int Gem;




}
