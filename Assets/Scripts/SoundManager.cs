using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioClip[] itemTake;

    public void ItemTake(int index) 
    {
        soundSource.PlayOneShot(itemTake[index]);
    }
}
