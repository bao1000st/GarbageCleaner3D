using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class GameData
{
    public int[] highscores;
    public string[] players;
    public GameData()
    {
        this.highscores = new int[10];
        this.players = new string[10];
    }

}
