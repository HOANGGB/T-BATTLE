using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : Item
{
    public override void TakeByBot(GameObject bot)
    {
        var bott =  bot.GetComponent<Bot>();
        bott.heal += bott.tankCurrent.heal / 20;
        bott.healMax += bott.tankCurrent.heal /20;

        bott.uIBot.UpdateHeal(bott.heal,bott.healMax);
    }

    public override void TakeByPlayer(GameObject player)
    {
        var tank = player.GetComponent<TankPlayer>();
        tank.healCurrent += tank.tankCurrent.heal / 20;
        tank.healMax += tank.tankCurrent.heal / 20;

        tank.uITank.UpdateHealBar(tank.healCurrent,tank.healMax);
    }
}
