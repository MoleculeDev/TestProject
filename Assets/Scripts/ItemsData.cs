using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsData 
{
    public List<Item> itemsList;
}

[System.Serializable]
public class Item 
{
    public string itemName;
    public int itemCost;
    public int countOfItem;
    public Sprite itemSprite;
}
