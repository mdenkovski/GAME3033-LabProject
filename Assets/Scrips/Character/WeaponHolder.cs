using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon To Spawn")]
    [SerializeField]
    private GameObject WeaponToSpawn;

    [SerializeField]
    private Transform WeaponSocketLocation;


    private Transform GripIKLocation;


    private bool WasFiring = false;
    private bool FiringPressed = false;

    //components
    public PlayerController Controller => PlayerController;
    private PlayerController PlayerController;
    private CrossHairScript PlayerCrosshair;
    private Animator PlayerAnimator;

    //Ref
    private Camera ViewCamera;
    private WeaponComponent EquippedWeapon;


    private static readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private static readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private static readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
    private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    private static readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");

    private void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
        if (PlayerController)
        {
            PlayerCrosshair = PlayerController.CrossHair;
        }

        ViewCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
       GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocketLocation.position, WeaponSocketLocation.rotation, WeaponSocketLocation);

        if (spawnedWeapon)
        {
            EquippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
            if (EquippedWeapon)
            {
                EquippedWeapon.Initialize(this, PlayerCrosshair);

                PlayerEvents.Invoke_OnWEaponEquipped(EquippedWeapon);

                GripIKLocation = EquippedWeapon.GripLocation;
                PlayerAnimator.SetInteger(WeaponTypeHash, (int)EquippedWeapon.WeaponInformation.WeaponType);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
    }


    public void OnReload(InputValue pressed)
    {

        StartReloading();

    }

    public void StartReloading()
    {

        if (EquippedWeapon.WeaponInformation.BulletsAvailable <= 0 && PlayerController.IsFiring)
        {
            StopFiring();
            return;
        }

        if (PlayerController.IsFiring)
        {
            WasFiring = true;
            StopFiring();

            

        }

        PlayerController.IsReloading = true;
        PlayerAnimator.SetBool(IsReloadingHash, true);
        EquippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0.0f, 0.1f);
    }

    public void StopReloading()
    {
        if (PlayerAnimator.GetBool(IsReloadingHash)) return;

        PlayerController.IsReloading = false;
        EquippedWeapon.StopReloading();
        CancelInvoke(nameof(StopReloading));

        if (WasFiring && FiringPressed)
        {
            StartFiring();
            WasFiring = false;
        }
    }


    public void OnFire(InputValue pressed)
    {
        FiringPressed = pressed.isPressed;
        if (pressed.isPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
        //Debug.Log("Firing");
    }
    private void StartFiring()
    {
        //TODO: wepon seems to reload after no bullets left
        if (EquippedWeapon.WeaponInformation.BulletsAvailable <= 0 && EquippedWeapon.WeaponInformation.BulletsInClip <= 0) return;
        PlayerController.IsFiring = true;
        PlayerAnimator.SetBool(IsFiringHash, true);
        EquippedWeapon.StartFiringWeapon();
    }


    private void StopFiring()
    {
        PlayerController.IsFiring = false;
        PlayerAnimator.SetBool(IsFiringHash, false);
        EquippedWeapon.StopFiringWeapon();

    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(PlayerCrosshair.CurrentAimPosition);
        //Debug.Log(independentMousePosition);

        PlayerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
        PlayerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);

    }

    

}
