using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGenerator : MonoBehaviour
{
    [Header("Scripts")]
    public GamePlayController gamePlayController;

    [Header("GameObjects")]
    public GameObject itemPrefab;
    public GameObject moneyPrefab;
    
    [Header("Transforms")]
    public List<Transform> itemTransforms;
    public List<Transform> moneyTransorms;

    [Header("GameObjects list")]
    public List<GameObject> itemsList;
    public List<GameObject> moneyList;

    [Header("Ints and Floats")]
    public int recievedMoney;
    public int waitingSecond;

    [Header("Bools")]
    public bool reciever;

    private void Start()
    {
        StartCoroutine(ItemGenerating());
        StartCoroutine(MoneyCreate());
    }

    public IEnumerator ItemGenerating() 
    {
        while (gameObject && !reciever) 
        {
            if (itemsList.Count < itemTransforms.Count) 
            {
                GameObject item = Instantiate(itemPrefab, itemTransforms[itemsList.Count]);
                itemsList.Add(item);

                yield return new WaitForSeconds(waitingSecond);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator MoneyCreate() 
    {
        int moneyCount = Random.Range(3, 10);

        while(moneyCount > 0) 
        {
            GameObject money = Instantiate(moneyPrefab, moneyTransorms[moneyList.Count]);
            moneyList.Add(money);
            moneyCount--;

            yield return new WaitForSeconds(0.05f);
        }
    }

}
