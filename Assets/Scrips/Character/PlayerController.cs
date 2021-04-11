using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


[RequireComponent(typeof(PlayerHealthComponent))]
public class PlayerController : MonoBehaviour, IPausable, ISavable
{

    public bool IsFiring;
    public bool IsReloading;
    public bool IsJumping;
    public bool IsRunning;
    public bool InInventory;
    public CrossHairScript CrossHair => CrossHairComponent;
    [SerializeField] private CrossHairScript CrossHairComponent;


    public HealthComponent Health => HealthComponent;
    [SerializeField] private HealthComponent HealthComponent;

    public InventoryComponent Inventory => InventoryComponent;
    [SerializeField] private InventoryComponent InventoryComponent;


    public WeaponHolder WeaponHolder => WeaponHolderComponent;
    [SerializeField] private WeaponHolder WeaponHolderComponent;

   // [SerializeField]
   // private ConsummableScript Consume;


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
        if (Inventory == null)
        {
            InventoryComponent = GetComponent<InventoryComponent>();
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

    public void OnInventory(InputValue button)
    {
        Debug.Log("Inventory Selected");
        if (InInventory)
        {
            OpenInventory(false);
        }
        else
        {
            OpenInventory(true);

        }

        InInventory = !InInventory;
        
    }

    private void OpenInventory(bool open)
    {
        if (open)
        {
            PauseManager.Instance.PauseGame();
            UIController.EnableInventoryMenu();

        }
        else
        {
            PauseManager.Instance.UnPauseGame();
            UIController.EnableGameMenu();

        }
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
    public void OnSaveGame(InputValue button)
    {
        SaveSystem.Instance.SaveGame();
    }

    public void OnLoadGame(InputValue button)
    {
        SaveSystem.Instance.LoadGame();

    }


    public SaveDataBase SaveData()
    {
        Transform playerTransform = transform;
        PlayerSaveData saveData = new PlayerSaveData
        {
            Name = gameObject.name,
            CurrentHealth = Health.Health,
            Position = playerTransform.position,
            Rotation = playerTransform.rotation
        };

        List<ItemSaveData> ItemSaveList = Inventory.GetItemList().Select(
            item => new ItemSaveData(item)).ToList();

        saveData.ItemList = ItemSaveList;


        saveData.EquippedWeapon = !WeaponHolder.EquippedWeapon
            ?null : new weaponSaveData(WeaponHolder.EquippedWeapon.WeaponInformation);

        return saveData;
    }

    public void LoadData(SaveDataBase saveData)
    {
        PlayerSaveData playerData = (PlayerSaveData)saveData;

        if (playerData == null) return;

        Transform playerTransform = transform;
        playerTransform.position = playerData.Position;

        playerTransform.rotation = playerData.Rotation;

        Health.SetCurrentHealth(playerData.CurrentHealth);

        foreach (ItemSaveData itemSaveData in playerData.ItemList)
        {
            ItemScriptable item = InventoryReferences.Instance.GetItemReference(itemSaveData.Name);
            Inventory.AddItem(item, itemSaveData.Amount);
        }
        if (playerData.EquippedWeapon == null) return;

        WeaponScriptable weapon = (WeaponScriptable)Inventory.FindItem(playerData.EquippedWeapon.Name);

        if (!weapon) return;
        weapon.WeaponStats = playerData.EquippedWeapon.WeaponStats;

        WeaponHolder.EquipWeapon(weapon);
    }
}

[Serializable]
public class PlayerSaveData: SaveDataBase
{
    public float CurrentHealth;
    public Vector3 Position;
    public Quaternion Rotation;


    public List<ItemSaveData> ItemList = new List<ItemSaveData>();
    public weaponSaveData EquippedWeapon;
}