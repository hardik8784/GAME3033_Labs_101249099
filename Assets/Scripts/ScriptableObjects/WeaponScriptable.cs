using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Items/Weapon",order =2)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if(eqipped)
        {
            //uneqip from inventory
            //remove from controller
        }
        else
        {
            //invoke onweaponeuipped from player
            //equip weapon from weaponholder
        }

        base.UseItem(playerController);
    }
}
