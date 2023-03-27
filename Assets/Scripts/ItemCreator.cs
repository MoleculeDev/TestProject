using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Creator", menuName = ("Item"))]
public class ItemCreator : ScriptableObject
{
    public string itemName;
    public int itemCost;
    public Sprite itemSprite;
}
