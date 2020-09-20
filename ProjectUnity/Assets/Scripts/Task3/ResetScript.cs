using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScript : MonoBehaviour
{
    public GameObject errorText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rectangle")
        {
            other.gameObject.transform.position = new Vector3(200, other.gameObject.transform.position.y, 0);
            errorText.SetActive(true);
        }
    }
}
