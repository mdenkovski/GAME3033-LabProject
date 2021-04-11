using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryReferences : MonoBehaviour
{
    public static InventoryReferences Instance;


    [SerializeField]
    List<ItemScriptable> ItemList = new List<ItemScriptable>();

    private readonly Dictionary<string, ItemScriptable> ItemsDictionary = new Dictionary<string, ItemScriptable>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach (ItemScriptable itemScriptable in ItemList)
        {
            ItemsDictionary.Add(itemScriptable.Name, itemScriptable);
        }

    }

    public ItemScriptable GetItemReference(string itemName) => ItemsDictionary.ContainsKey(itemName) ? ItemsDictionary[itemName] : null;

}
