using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DataPersistenceManager : MonoBehaviour
{
    public string fileName;
    public GameData gameData;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Application.persistentDataPath);
        LoadGame();
    }

    private void OnApplicationQuit() {
        SaveGame();    
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    private void LoadGame()
    {
        gameData = fileDataHandler.Load();
        if (gameData == null)
        {
            Debug.Log("no data was found. Initializing data to defaults.");
            NewGame();
        }
        Debug.Log("load highscores");
    }

    private void SaveGame()
    {
        fileDataHandler.Save(gameData);
        Debug.Log("save highscore");
    }

    public bool checkHighscore(int newScore)
    {
        if (newScore==0) return false;
        for (int i=0; i< gameData.highscores.Length;i++)
        {
            if (newScore > gameData.highscores[i])
            {
                return true;
            }   
        }
        return false;
    }
    
    public void saveHighscore(string name, int newScore)
    {
        var tempHighscores = new int[11];
        tempHighscores = gameData.highscores.Concat(new int[1] {newScore}).ToArray();
        var tempPlayers = new string[11];
        tempPlayers = gameData.players.Concat(new string[1] {name}).ToArray();

        for (int i=0;i<tempHighscores.Length;i++)
        {
            for (int j=i+1;j<tempHighscores.Length;j++)
            {
                if (tempHighscores[i] < tempHighscores[j])
                {
                    var temp1 = tempHighscores[i];
                    tempHighscores[i] = tempHighscores[j];
                    tempHighscores[j] = temp1;
                    
                    var temp2 = tempPlayers[i];
                    tempPlayers[i] = tempPlayers[j];
                    tempPlayers[j] = temp2;

                }
            }
        }

        for (int i=0;i<gameData.highscores.Length;i++)
        {
            gameData.highscores[i] = tempHighscores[i];
            gameData.players[i] = tempPlayers[i];
        }
        SaveGame();
    }
        
}
