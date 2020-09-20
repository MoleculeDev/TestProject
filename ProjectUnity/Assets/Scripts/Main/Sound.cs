using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ballSpawn, ballPop;

    public void BallSpawn() 
    {
        audioSource.PlayOneShot(ballSpawn);
    }
    
    public void BallPop() 
    {
        audioSource.PlayOneShot(ballPop);
    }
    
}
