using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rectangle")
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -35);
            
            if (other.gameObject.transform.localScale.z > 69) 
            {
                other.gameObject.transform.localScale = new Vector3(other.gameObject.transform.localScale.x, other.gameObject.transform.localScale.y, 68);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rectangle" && other.gameObject.transform.position.y > 2)
        {
            //other.gameObject.transform.position = new Vector3(200, other.gameObject.transform.position.y, 0);
        }
    }

}
