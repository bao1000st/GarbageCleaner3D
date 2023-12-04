using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public Image loadingBar;
    public Image loadingFill;
    public DataPersistenceManager dataPersistenceManager;
    public TMPro.TextMeshProUGUI leaderboardInfoText;
    public GameObject inputField;
    public GameObject retryButton;
    public GameObject backButton;

    public AudioSource source;
    public AudioClip clickSound;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderboardInfoText.text == "") ShowLeaderboardInfo();
    }

    void OnEnable() {
        ShowLeaderboardInfo();
    }
    
    public void Back()
    {
        source.PlayOneShot(clickSound);
        int children = transform.childCount;
        for (int i = 1; i < children-1; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(0));
    }
    public void ShoweEndUI()
    {
        source.PlayOneShot(clickSound);
        ShowLeaderboardInfo();
        retryButton.SetActive(true);
        backButton.SetActive(true);
        inputField.SetActive(false);
    }
    void ShowLeaderboardInfo()
    {
        leaderboardInfoText.text = "";
        for (int i=0; i<dataPersistenceManager.gameData.highscores.Length;i++)
        {
            if (dataPersistenceManager.gameData.highscores[i] != 0)
                leaderboardInfoText.text += (i+1)+". "+ dataPersistenceManager.gameData.players[i] + "          "+dataPersistenceManager.gameData.highscores[i]+"\n";
        }
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingBar.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingFill.fillAmount = progressValue;

            yield return null;
        }
    }


}
