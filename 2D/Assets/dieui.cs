using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            GhostController.restart = true;
            SceneManager.LoadScene("Scene_1");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
