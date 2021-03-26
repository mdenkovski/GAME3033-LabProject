using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerHealthComponent))]
public class PlayerController : MonoBehaviour, IPausable
{

    public bool IsFiring;
    public bool IsReloading;
    public bool IsJumping;
    public bool IsRunning;
    public CrossHairScript CrossHair => CrossHairComponent;
    [SerializeField] private CrossHairScript CrossHairComponent;


    public HealthComponent Health => HealthComponent;
    [SerializeField] private HealthComponent HealthComponent;

    public WeaponHolder WeaponHolder => WeaponHolderComponent;
    [SerializeField] private WeaponHolder WeaponHolderComponent;




    private GameUIController UIController;

    private PlayerInput PlayerInput;
    private void Awake()
    {
        UIController = FindObjectOfType<GameUIController>();
        PlayerInput = GetComponent<PlayerInput>();
        if (Health == null)
        {
            HealthComponent = GetComponent<HealthComponent>();
        }
        if (WeaponHolder == null)
        {
            WeaponHolderComponent = GetComponent<WeaponHolder>();
        }
    }

    public void OnPauseGame(InputValue value)
    {
        Debug.Log("Pause Game");
        PauseManager.Instance.PauseGame();
    }

    public void OnUnPauseGame(InputValue value)
    {
        Debug.Log("UnPause Game");
        PauseManager.Instance.UnPauseGame();

    }

    public void PauseMenu()
    {
        UIController.EnablePauseMenu();
        PlayerInput.SwitchCurrentActionMap("PauseActionMap");
    }

    public void UnPauseMenu()
    {
        UIController.EnableGameMenu();
        PlayerInput.SwitchCurrentActionMap("PlayerActionMap");
    }
}
