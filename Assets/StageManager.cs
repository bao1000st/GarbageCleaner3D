using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageManager : MonoBehaviour
{
    public float timer=60;
    private int totalScore=0;
    private bool gameEnded = false;
    public TrashMaker trashMaker3D;
    public TrashMaker trashMaker2D;
    public DataPersistenceManager dataPersistenceManager;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timerText; 
    public TMPro.TextMeshProUGUI gameOverText;

    public GameObject retryButton;
    public GameObject backButton;
    public GameObject backButton2;
    public GameObject leaderBoard;

    public AudioSource source;
    public AudioClip clickSound;
    public Image loadingBar;
    public Image loadingFill;

    public TMP_InputField scoreInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (ApplicationModel.use2DModels)
        {
            trashMaker2D.gameObject.SetActive(true);
            trashMaker3D.gameObject.SetActive(false);
        }
        else 
        {
            trashMaker2D.gameObject.SetActive(false);
            trashMaker3D.gameObject.SetActive(true);
        }
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.0f && gameEnded==false)
        {
            TimerEnded();
        }
        else
        {
            timerText.text = "Time: " + Mathf.RoundToInt(timer).ToString();
            scoreText.text = "Score: " + totalScore.ToString();
            timer -= Time.deltaTime;
        }
    }

    void TimerEnded()
    {
        gameEnded = true;
        if (dataPersistenceManager.checkHighscore(totalScore) == true)
        {
            leaderBoard.SetActive(true);
        }
        else
        {
            gameOverText.text = "GAME OVER!!!";
            retryButton.SetActive(true);
            backButton.SetActive(true); 
        }
        backButton2.SetActive(false);
        trashMaker2D.gameObject.SetActive(false);
        trashMaker3D.gameObject.SetActive(false);
        
    }

    public void GainScore(int score)
    {
        totalScore += score;
    }

    public void Restart()
    {
        source.PlayOneShot(clickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        source.PlayOneShot(clickSound);
        transform.GetChild(0).gameObject.SetActive(true);
        int children = transform.childCount;
        for (int i = 1; i < children-1; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(0));
    }

    public void UpdateLeaderboard()
    {
        dataPersistenceManager.saveHighscore(scoreInputField.text,totalScore);
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
