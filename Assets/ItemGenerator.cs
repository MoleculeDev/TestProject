using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGenerator : MonoBehaviour
{
    public GamePlayController gamePlayController;
    public GameObject itemPrefab;
    public List<Transform> itemTransforms;
    public List<GameObject> itemsList;
    public int waitingSecond;
    public bool reciever;

    private void Start()
    {
        StartCoroutine(ItemGenerating());
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

}
