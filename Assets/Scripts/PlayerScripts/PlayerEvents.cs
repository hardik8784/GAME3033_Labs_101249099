using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public delegate void OnWeaponEquippedEvent(WeaponComponent weaponComponent);

    public static event OnWeaponEquippedEvent OnWeaponEquipped;

    public static void InvokeOnWeaponEquipped(WeaponComponent weaponComponent)
    {
        OnWeaponEquipped?.Invoke(weaponComponent);
    }

    public delegate void OnHealthInitializedEvent(HealthComponent healthComponent);

    public static event OnHealthInitializedEvent onHealthInitialized;

    public static void InvokeOnHealthInitialized(HealthComponent healthComponent)
    {
        onHealthInitialized?.Invoke(healthComponent);
    }
}
