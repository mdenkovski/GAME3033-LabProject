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

    //components
    private PlayerController PlayerController;
    private CrossHairScript PlayerCrosshair;
    private Animator PlayerAnimator;

    //Ref
    private Camera ViewCamera;
    private static readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private static readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private static readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
    private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");

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
            WeaponComponent weapon = spawnedWeapon.GetComponent<WeaponComponent>();
            if (weapon)
            {
                GripIKLocation = weapon.GripLocation;
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
        //Debug.Log("Reloading");
        PlayerAnimator.SetBool(IsReloadingHash, pressed.isPressed);
    }

    public void OnFire(InputValue pressed)
    {

        //Debug.Log("Firing");
        PlayerAnimator.SetBool(IsFiringHash, pressed.isPressed);
    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(PlayerCrosshair.CurrentAimPosition);
        //Debug.Log(independentMousePosition);

        PlayerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
        PlayerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);

    }
}
