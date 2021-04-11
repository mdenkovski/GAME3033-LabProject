using UnityEngine;
using System;

[CreateAssetMenu(fileName ="Weapon", menuName ="Items/Weapon", order =2)]
public class WeaponScriptable : EquipableScriptable
{
    public WeaponStats WeaponStats;

    public override void UseItem(PlayerController controller)
    {
        if (Equipped)
        {
            controller.WeaponHolder.UnEquipItem();
        }
        else
        {
            controller.WeaponHolder.EquipWeapon(this);
        }

        base.UseItem(controller);
    }
}
[Serializable]
public class weaponSaveData : SaveDataBase
{

    public WeaponStats WeaponStats;

    public weaponSaveData(WeaponStats weaponStats)
    {
        Name = weaponStats.WeaponName;
        WeaponStats = weaponStats;
    }
}