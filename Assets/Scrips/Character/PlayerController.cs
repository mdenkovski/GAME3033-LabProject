using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CrossHairScript CrossHair => CrossHairComponent;
    [SerializeField] private CrossHairScript CrossHairComponent;

    public bool IsFiring;
    public bool IsReloading;
    public bool IsJumping;
    public bool IsRunning;

}
