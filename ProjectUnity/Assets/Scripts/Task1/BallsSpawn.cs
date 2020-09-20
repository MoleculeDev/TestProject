using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallsSpawn : MonoBehaviour
{
    public GameObject Ball,Cube;
    private bool GameOver;
    private int ObjectCount;
    private int ObjectCount2;
    public Text ObjectCountText;
    public Text ObjectCount2Text;
    public Text destroyText;
    public int destroyedBall;
    public Sound sound;
    public int randomObject;
    public int Task;
    public Text TaskNumber;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Task = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) 
        {
            Task = 2;
        }
        TaskNumber.text = "Task Number: " + Task.ToString();
        GameOver = false;
        StartCoroutine(BallSpawn());

    }
    public void StopGame() 
    {
        GameOver = true;
    }

    IEnumerator BallSpawn() 
    {
        if (Task == 2)
        {
            while (GameOver == false)
            {
                randomObject = Random.Range(0, 10);

                if (randomObject < 6)
                {
                    Instantiate(Ball, new Vector3(Random.Range(-47, 47), 15, Random.Range(-47, 47)), Quaternion.identity);
                    sound.BallSpawn();
                    yield return new WaitForSeconds(3.0f);
                }

                else 
                {
                    Instantiate(Cube, new Vector3(Random.Range(-47, 47), 15, Random.Range(-47, 47)), Quaternion.identity);
                    sound.BallSpawn();
                    yield return new WaitForSeconds(3.0f);
                }
            }
        }
        else if (Task == 1) 
        {
            while (GameOver == false)
            {
                 Instantiate(Ball, new Vector3(Random.Range(-47, 47), 15, Random.Range(-47, 47)), Quaternion.identity);
                 sound.BallSpawn();
                 yield return new WaitForSeconds(3.0f);
            }

        }
        StartCoroutine(BallSpawn());
    }

    private void CountObjects()
    {
        if (Task == 1)
        {
            ObjectCount = GameObject.FindGameObjectsWithTag("Ball").Length + GameObject.FindGameObjectsWithTag("newBall").Length;
            ObjectCountText.text = "Count of balls: " + ObjectCount.ToString();
        }

        if (Task == 2)
        {
            ObjectCount = GameObject.FindGameObjectsWithTag("Ball").Length + GameObject.FindGameObjectsWithTag("newBall").Length;
            ObjectCountText.text = "Count of balls: " + ObjectCount.ToString();
            ObjectCount2 = GameObject.FindGameObjectsWithTag("Cube").Length + GameObject.FindGameObjectsWithTag("newCube").Length;
            ObjectCount2Text.text = "Count of rectangles: " + ObjectCount2.ToString();

        }
    }

    public void CountDestroy() 
    {
        destroyText.text = "Destroyed by wall: " + destroyedBall.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        randomObject = Random.Range(0, 10);
        CountObjects();
        CountDestroy();

        print(randomObject);
    }
}
