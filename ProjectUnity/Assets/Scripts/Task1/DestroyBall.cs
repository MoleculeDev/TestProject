using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    public Sound sound;
    public BallsSpawn ballSpawnScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Cube" || other.gameObject.tag == "newCube" || other.gameObject.tag == "newBall") 
        {
            ballSpawnScript.destroyedBall++;
            sound.BallPop();
            Destroy(other.gameObject);
        }
    }
}
