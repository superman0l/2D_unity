using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindGameObjectsWithTag("Global").Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
