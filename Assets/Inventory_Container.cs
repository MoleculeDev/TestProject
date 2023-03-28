using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Container : MonoBehaviour
{
    public List<Cell> containers;

    public void CheckCells() 
    {
        for (int i = 0; i < containers.Count; i++)
        {
            if (containers[i].countOfItem == 0) containers[i].parentGameObject.SetActive(false);
            else 
            {
                containers[i].countText.text = containers[i].countOfItem.ToString();
            }
        }
    }

    private void OnEnable()
    {
        //CheckCells();
    }
}

[System.Serializable]
public class Cell 
{
    public GameObject parentGameObject;
    public Text countText;
    public int countOfItem;
    public Image resourceIcon;
}
