using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public SoundManager soundManager;

    [Header("Particle Systems")]
    public ParticleSystem poofEffect;
    public GameData gameData;



    #region Particle Systems region
    public void PoofEffect(Vector3 position) 
    {
        Instantiate(poofEffect, position, Quaternion.identity);
    }
    #endregion
}
