using UnityEngine;
using System;

[System.Serializable]
public class GameData 
{
    //Main values
    public int money;

    //Player values
    public Vector3 playerPosition;
    public int playerMaxContains;
    public int playerMaxHealth;
    public int playerLevel;
    public int playerSpeed;

    //Sound data
    public bool soundEnabled;
    public bool musicIsEnabled;
    public float soundValue;
    public float musicValue;

    //Tutorial
    public bool tutorialIsActive;
}
