using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform tf;
    public Transform playertransform;
    public double limit_l, limit_r, limit_g, limit_b;
    public float someground;
    public float init_x, init_y;
    // Start is called before the first frame update
    void Start()
    {
        tf.position = new Vector3(init_x, init_y, tf.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }
    void CameraFollow() {
        if (playertransform.position.x >= limit_l && playertransform.position.x <= limit_r && playertransform.position.y >= limit_b - someground && playertransform.position.y <= limit_g - someground)
            tf.position = new Vector3(playertransform.position.x, playertransform.position.y + someground, tf.position.z);
        else if (playertransform.position.x >= limit_l && playertransform.position.x <= limit_r)
            tf.position = new Vector3(playertransform.position.x, tf.position.y, tf.position.z);
        else if (playertransform.position.y >= limit_b - someground && playertransform.position.y <= limit_g - someground)
            tf.position = new Vector3(tf.position.x, playertransform.position.y + someground, tf.position.z);
    }
}
