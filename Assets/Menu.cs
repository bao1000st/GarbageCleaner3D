using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image loadingBar;
    public Image loadingFill;
    public AudioSource source;
    public AudioClip clickSound;
    public Toggle toggle;

    void Start()
    {
        ApplicationModel.use2DModels = false;
    }


    public void StartGame()
    {
        source.PlayOneShot(clickSound);
        int children = transform.childCount;
        for (int i = 1; i < children - 1; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(2));
    }

    public void OpenLeaderboard()
    {
        source.PlayOneShot(clickSound);
        int children = transform.childCount;
        for (int i = 1; i < children - 1; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(LoadSceneAsync(1));
    }

    public void ExitGame()
    {
        source.PlayOneShot(clickSound);
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingBar.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingFill.fillAmount = progressValue;

            yield return null;
        }
    }

    public void SetUse2DModels()
    {
        source.PlayOneShot(clickSound);
        ApplicationModel.use2DModels = !ApplicationModel.use2DModels;
    }

    public void PlayVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }
}
