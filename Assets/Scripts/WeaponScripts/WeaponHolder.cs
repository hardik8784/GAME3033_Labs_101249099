using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject WeaponToSpawn;

    //Components
    public PlayerController playerController;
  
    Animator Playeranimator;

    public Sprite CrossHairImage;
    WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject WeaponSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    
    bool firingPressed = false;

    public readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    public readonly int IsReloadingHash = Animator.StringToHash("IsReloading");

    // Start is called before the first frame update
    void Start()
    {
        Playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        GameObject SpawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocketLocation.transform.position, WeaponSocketLocation.transform.rotation,WeaponSocketLocation.transform);
        
        equippedWeapon = SpawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
     
        if(!playerController.isReloading)
        {
            Playeranimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            Playeranimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
        }
       
    }

   

    public void OnFire(InputValue value)
    {
        //playerController.isFiring = value.isPressed;
        firingPressed = value.isPressed;
        if(firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0) return;

        //Set up Firing Animation
        Playeranimator.SetBool(IsFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    void StopFiring()
    {
        //Set up Firing Animation to false
        Playeranimator.SetBool(IsFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;

        //Setup Reload Animation
        Playeranimator.SetBool(IsReloadingHash, playerController.isReloading);
        Playeranimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
    }

    public void StartReloading()
    {

    }
}
