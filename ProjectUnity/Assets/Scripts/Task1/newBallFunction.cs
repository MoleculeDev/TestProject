using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newBallFunction : MonoBehaviour
{
    private Vector3 ObjectMaxRadius;
    private Vector3 ObjectMinRadius;
    private float speed = 1.1f;
    public Sound sound;


    // Start is called before the first frame update
    void Start()
    {
        ObjectMinRadius = transform.localScale;
        ObjectMaxRadius = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.y * 1.2f);

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Cube")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, ObjectMaxRadius, Time.deltaTime * speed);
        if (transform.localScale == ObjectMaxRadius)
        {
            Destroy(gameObject);
        }

    }
}
