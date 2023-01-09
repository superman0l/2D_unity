using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEUI : MonoBehaviour
{
    public GameObject tt;
    float counttime =  0f;
    //public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        tt = GameObject.Find("Text");
        //anim = GameObject.Find("Treasure_Dash").GetComponent<Animator>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        blink();
    }
    public void blink() {
        counttime += Time.deltaTime * 2;
        if (counttime % 2 >= 1.0f)
        {
            tt.SetActive(true);
        }
        else
        {
            tt.SetActive(false);
        }
    }
}
