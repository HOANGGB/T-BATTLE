using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tank", menuName = "ScriptableObjects/Tank",order = 1)]
public class Tank : ScriptableObject
{
    
    public int ID;
   public string Name;
   public bool Status;
   public int Level;
   public Sprite Image;
   public Sprite ImageSkill;
   public TankRarity TankRarity;
   public int heal;
   public int damage;
   public int damageSkill;

   public int Pirce;
   public int Magazine;
   public int ShootingRange;
   public float WidthRange;
   
   public float Speed;
   public float SpeedBullet;
   public float SpeedReload;
   public float SpeedReloadSkill;
   public float SpeedRotation;
   public string Describe;
   public AudioClip audioFire;
   public AudioClip audioSkill;
   public GameObject OBJ_Tank;
   public List<int> ListPriceUpgrade;
}

public enum TankRarity
{
    Common,     // Thường
    Rare,       // Hiếm
    Epic        // Cực phẩm
}