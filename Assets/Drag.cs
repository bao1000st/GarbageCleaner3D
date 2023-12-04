using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    
    public string trashType = "";
    public int directionSpeed = 1;
    public int rotationSpeed = 1;
    public int score = 1;
    public bool is2D = false;
    
    private bool drag;
    private Vector3 originalPosition;
    private Vector3 mousePosition;
    private string trashTypeOfBin = "";
    private TrashMaker trashMaker;
    private Bin bin;
    private BoxCollider collider;
    private Vector3 colliderSize;
    private Vector3 scale;


    void Start()
    {
        trashMaker = transform.parent.GetComponent<TrashMaker>();
        collider = transform.GetComponent<BoxCollider>();
        colliderSize = collider.size;
        collider.size = new Vector3(collider.size.x + collider.size.x*0.9f,collider.size.y + collider.size.y*0.9f,collider.size.z + collider.size.z*0.9f);
        scale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x + transform.localScale.x*0.8f,transform.localScale.y + transform.localScale.y*0.8f,transform.localScale.z + transform.localScale.z*0.8f);
    }

    void FixedUpdate()
    {
        if (drag == false)
        {
            originalPosition = transform.position;
            if (is2D == true) transform.Rotate(0,0,rotationSpeed,Space.Self);
            else transform.Rotate(rotationSpeed,0,0,Space.Self); //rotate
            transform.Translate (-directionSpeed*0.01f,0,0,Space.World);  //move 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bin")
        {
            bin = collision.gameObject.GetComponent<Bin>();
            trashTypeOfBin = bin.trashType;
        }
    }

    private void OnMouseDown()
    {
        if (drag==false)
        {
            drag=true;
            BoxCollider collider = transform.GetComponent<BoxCollider>();
            collider.size = colliderSize;
            //transform.localScale = scale;
            Vector3 subtract = Camera.main.transform.position - transform.position;
            transform.Translate (subtract.x*0.6f,subtract.y*0.6f,subtract.z*0.6f,Space.World);  //move
            mousePosition = Input.mousePosition - GetObjPos();
        }
    }

    private void OnMouseDrag() {
        float originalY = transform.position.y;
        transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        drag = false;
        if (trashTypeOfBin != "")
        {
            bin.animator.SetBool("Open",false);
            Destroy(this.gameObject);
            if (trashType == trashTypeOfBin)
            {
                trashMaker.stageManager.GainScore(score);
                trashMaker.PlayCorrectSound();
                bin.ShowFloatingText(score.ToString());
            }
            else
            {
                trashMaker.PlayIncorrectSound();
                bin.ShowFloatingText("X");
            }
            trashTypeOfBin = "";
        }
        else
        {
            collider.size = new Vector3(collider.size.x + collider.size.x*0.9f,collider.size.y + collider.size.y*0.9f,collider.size.z + collider.size.z*0.9f);
            transform.localScale = new Vector3(transform.localScale.x + transform.localScale.x*0.7f,transform.localScale.y + transform.localScale.y*0.7f,transform.localScale.z + transform.localScale.z*0.7f);
            transform.position = originalPosition;
        }
    }

    private Vector3 GetObjPos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
