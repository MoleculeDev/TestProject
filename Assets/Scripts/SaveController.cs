using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        SaveSystem.Initialize();
        SaveSystem.LoadInventory();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SaveInvetory() 
    {
        string inventoryData = JsonUtility.ToJson(player.inventory);
        SaveSystem.SaveInventory(inventoryData);
        print("Inventory Saved");
    }

    public void LoadInventory() 
    {
        string inventory = SaveSystem.LoadInventory();

        if (inventory != null)
        {
            player.inventory = JsonUtility.FromJson<InventoryHandler>(inventory);
            print("Inventory Loaded");
        }

        else
        {
            SaveInvetory();
        }

        ///generatorsUpdater.SetGeneratorsData();
    }
}
