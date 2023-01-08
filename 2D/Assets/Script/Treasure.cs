using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Animator anim;
    public Transform tf;
    public GameObject EUI;
    public GameObject dashtip;
    public bool opened = false;
    public GameObject dis;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckKey();
        //if (opened) dis.SetActive(false);
    }
    public void AnimationAct1()
    {
        tf.position = new Vector3(tf.position.x, tf.position.y + 0.2f, tf.position.z);
    }
    public void AnimationAct2()
    {
        tf.position = new Vector3(tf.position.x + 0.1f, tf.position.y + 0.1f, tf.position.z);
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (!opened && collision.tag == "Global")
        {
            EUI.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!opened && collision.tag == "Global")
        {
            EUI.SetActive(false);
        }
    }
    public void CheckKey()
    {
        if (!opened && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("open", true);
            EUI.SetActive(false);
            dashtip.SetActive(true);
            opened= true; 
        }
        else if(opened && Input.anyKeyDown)
        {
            dashtip.SetActive(false);
        }
    }
}
