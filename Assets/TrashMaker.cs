using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMaker : MonoBehaviour
{
    public StageManager stageManager;
    public float minTimer=0.5f;
    public float maxTimer=2.0f;
    public int destroyTime = 5;
    private float timer=0;
    public GameObject[] trashObjects;

    public AudioSource source;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            int randomIndex = Random.Range(0,trashObjects.Length);
            GameObject clone = Instantiate(trashObjects[randomIndex], new Vector3(transform.position.x, transform.position.y, transform.position.z-Random.Range(0.0f,15.0f)), trashObjects[randomIndex].transform.rotation);
            clone.SetActive(true);
            clone.transform.SetParent(this.transform);
            Destroy(clone,destroyTime);

            timer = Random.Range(minTimer,maxTimer);
        }
    }

    public void PlayCorrectSound()
    {
        source.PlayOneShot(correctSound);
    }

    public void PlayIncorrectSound()
    {
        source.PlayOneShot(incorrectSound);
    }
}
