using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GamePlayController gamePlayController;
    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioClip[] itemTake;
    public AudioClip[] footSteps;

    //private void Start()
    //{
    //    soundSource.volume = gamePlayController.gameData.soundValue;
    //    soundSource.enabled = gamePlayController.gameData.soundEnabled;
    //    musicSource.volume = gamePlayController.gameData.musicValue;
    //    musicSource.enabled = gamePlayController.gameData.musicIsEnabled;
    //}

    public void ItemTake(int index) 
    {
        soundSource.PlayOneShot(itemTake[index]);
    }

    public void FootStep() 
    {
        soundSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
    }

}
