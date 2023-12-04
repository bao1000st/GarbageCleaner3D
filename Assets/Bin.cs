using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bin : MonoBehaviour
{
    public GameObject floatingText;
    public string trashType = "";
    private TextMeshPro text;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = trashType.ToUpper();
    }

    void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Open",true);
    }

    void OnCollisionExit(Collision other)
    {
        animator.SetBool("Open",false);
    }

    public void ShowFloatingText(string showText)
    {
        GameObject clone = Instantiate(floatingText, new Vector3(transform.position.x,transform.position.y+5.3f,transform.position.z), text.transform.rotation, transform);
        TextMeshPro fText = clone.GetComponent<TextMeshPro>();
        fText.text=showText;
        if (showText=="X")
        {
            fText.color = Color.red;
        }
        else
        {
            fText.color = Color.green;
        }
    }

}
