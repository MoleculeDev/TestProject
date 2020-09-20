using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallFunction : MonoBehaviour
{
    private Vector3 ObjectMaxRadius;
    private Vector3 ObjectMinRadius;
    private float speed = 1.1f;
    public GameObject newBall;
    public GameObject newCube;
    public Sound sound;
    public BallsSpawn ballsSpawn;






    // Start is called before the first frame update
    void Start()
    {
        ObjectMinRadius = transform.localScale;
        ObjectMaxRadius = new Vector3(transform.localScale.x * 2.0f, transform.localScale.y * 2.0f, transform.localScale.y * 2.0f);

    }
    private void OnCollisionEnter(Collision collision)
    {
            if (ballsSpawn.Task == 1)
            {
                if (collision.gameObject.tag == "Ball") 
                { 
                        //sound.BallPop();
                        Destroy(collision.gameObject);
                        Instantiate(newBall, collision.contacts[0].point, Quaternion.identity);
                        newBall.transform.localScale = collision.gameObject.transform.localScale + gameObject.transform.localScale;
                }
            }
            else if (ballsSpawn.Task == 2)
            {
                {
                    //sound.BallPop();
                    if (gameObject.tag == "Cube" && collision.gameObject.tag == "Cube")
                    {
                        if (ballsSpawn.randomObject < 6) 
                        {
                            Instantiate(newBall, collision.contacts[0].point, Quaternion.identity);
                            newBall.transform.localScale = new Vector3(gameObject.transform.localScale.x - 3, gameObject.transform.localScale.y, gameObject.transform.localScale.z) + new Vector3(collision.gameObject.transform.localScale.x - 3, collision.gameObject.transform.localScale.y, collision.gameObject.transform.localScale.z);
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                        else
                        {
                            Destroy(gameObject);
                            Destroy(collision.gameObject);
                            Instantiate(newCube, collision.contacts[0].point, Quaternion.identity);
                            newCube.transform.localScale = gameObject.transform.localScale + collision.transform.localScale;
                        }
                    }
                    else if (gameObject.tag == "Ball" && collision.gameObject.tag == "Ball")
                    {
                        if (ballsSpawn.randomObject < 6) 
                        {
                            Destroy(collision.gameObject);
                            Instantiate(newBall, collision.contacts[0].point, Quaternion.identity);
                            newBall.transform.localScale =  gameObject.transform.localScale + collision.gameObject.transform.localScale;
                        }
                        else 
                        {
                            Destroy(collision.gameObject);
                            Instantiate(newCube, collision.contacts[0].point, Quaternion.identity);
                            newCube.transform.localScale = new Vector3(gameObject.transform.localScale.x + 3, gameObject.transform.localScale.y, gameObject.transform.localScale.z) + collision.gameObject.transform.localScale;
                        }
                        
                    }
                    else if (gameObject.tag == "Cube" && collision.gameObject.tag == "Ball" || gameObject.tag == "Ball" && collision.gameObject.tag == "Cube") 
                    {
                        Destroy(collision.gameObject);
                    }

                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, ObjectMaxRadius, Time.deltaTime * speed );
        if (transform.localScale == ObjectMaxRadius)
        {
            //sound.BallPop();
            Destroy(gameObject);
        }
             
    }
}
