using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : Item
{


    public override void TakeByBot(GameObject bot)
    {
        var bott =  bot.GetComponent<Bot>();
        bott.damageBonus += bott.tankCurrent.damage / 20;

    }

    public override void TakeByPlayer(GameObject player)
    {
        var tank = player.GetComponent<TankPlayer>();
        tank.damageBonus += tank.tankCurrent.damage / 20;
    }
}
