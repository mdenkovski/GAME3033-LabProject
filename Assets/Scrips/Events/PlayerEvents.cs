using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents 
{
    public delegate void OnWEaponEquippedEvent(WeaponComponent weaponCompoent);


    public static event OnWEaponEquippedEvent OnWEaponEquipped;

    public static void Invoke_OnWEaponEquipped(WeaponComponent weaponCompoent)
    {
        OnWEaponEquipped?.Invoke(weaponCompoent);
    }
}
