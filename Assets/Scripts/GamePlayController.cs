using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public SoundManager soundManager;

    [Header("Particle Systems")]
    public ParticleSystem poofEffect;
    public GameData gameData;

    [Header("Floats and Ints")]
    public int money;

    [Header("UI panel")]
    public Text moneyText;
    public GameObject moneyText_Parent;
    public Text itemCounter;



    #region Particle Systems region
    public void PoofEffect(Vector3 position) 
    {
        Instantiate(poofEffect, position, Quaternion.identity);
    }
    #endregion
}
