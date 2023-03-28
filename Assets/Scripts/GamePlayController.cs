using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public SoundManager soundManager;

    [Header("Particle Systems")]
    public ParticleSystem poofEffect;
    public GameData gameData;

    [Header("Floats and Ints")]
    public int money;

    [Header("UI panel")]
    public GameObject invenotory;
    public Text moneyText;
    public GameObject moneyText_Parent;
    public Text itemCounter;

    [Header("Scripts")]
    public Player player;
    public Inventory_Container inventory_Container;
    public SaveController saveController;


    public void ShowInventory() 
    {
        invenotory.SetActive(true);

        for (int i = 0; i < player.inventory.itemsData.itemsList.Count; i++)
        {
            inventory_Container.containers[i].countOfItem = player.inventory.itemsData.itemsList[i].countOfItem;
            inventory_Container.containers[i].resourceIcon.sprite = player.inventory.itemsData.itemsList[i].itemSprite;
        }

        inventory_Container.CheckCells();

        //player.inventory.itemsData.itemsList
    }


    #region Particle Systems region
    public void PoofEffect(Vector3 position) 
    {
        Instantiate(poofEffect, position, Quaternion.identity);
    }
    #endregion
}
