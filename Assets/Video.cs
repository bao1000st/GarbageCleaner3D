using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Video : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        checkOver();
    }

    private void checkOver()
    {
        long playerCurrentFrame = transform.GetComponent<UnityEngine.Video.VideoPlayer>().frame;
        long playerFrameCount = Convert.ToInt64(transform.GetComponent<UnityEngine.Video.VideoPlayer>().frameCount);
     
        if(playerCurrentFrame < playerFrameCount-5)
        {
            Debug.Log("VIDEO IS PLAYING");
        }
        else
        {
            Debug.Log("VIDEO IS OVER");
            transform.parent.GetComponent<Menu>().StartGame();
        }
    }
}
