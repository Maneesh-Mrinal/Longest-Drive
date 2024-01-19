using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public float currentScore;

    public PlayerData(PlayerMovement player)
    {
        highScore = player.HighScore;
        currentScore = player.score;
    }
}
