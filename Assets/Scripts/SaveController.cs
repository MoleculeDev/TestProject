using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        SaveSystem.Initialize();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadInventory();
    }

    public void SaveInvetory() 
    {
        string inventoryData = JsonUtility.ToJson(player.inventory.itemsData);
        SaveSystem.SaveInventory(inventoryData);
        print("Inventory Saved");
    }

    public void LoadInventory() 
    {
        string inventory = SaveSystem.LoadInventory();

        if (inventory != null)
        {
            player.inventory.itemsData = JsonUtility.FromJson<ItemsData>(inventory);
            print("Inventory Loaded");
        }

        else
        {
            SaveInvetory();
        }

        ///generatorsUpdater.SetGeneratorsData();
    }
}
