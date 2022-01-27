using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject WeaponToSpawn;

    public Sprite CrossHairImage;

    [SerializeField]
    GameObject WeaponSocketLocation;

    // Start is called before the first frame update
    void Start()
    {
        GameObject SpawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocketLocation.transform.position, WeaponSocketLocation.transform.rotation,WeaponSocketLocation.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
