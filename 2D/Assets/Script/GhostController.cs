using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    //basic attributes of Player
    public Transform tf;
    public Rigidbody2D rb;
    //speed for horizontal move and jump
    public float speed=5f;
    //grounded check
    public bool istouchground;
    public Transform[] GroundChecks = new Transform[3];
    public LayerMask GroundLayer;
    //animation
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(tf);
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        istouchground = true;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        Jump();
        AirStateChange();
    }
    void HorizontalMove() 
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction < 0)
        {
            tf.eulerAngles = new Vector3(tf.rotation.x, 180, tf.rotation.z);
        }
        else if (direction > 0)
        {
            tf.eulerAngles = new Vector3(tf.rotation.x, 0, tf.rotation.z);
        }
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    void Jump() 
    {
        CheckOnGround();
        if (istouchground && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * 1.2f);
            AirStateChange();
        }
    }
    void CheckOnGround() {
        for (int i = 0; i < 3; i++) {
            istouchground = Physics2D.Linecast(transform.position, GroundChecks[i].position, GroundLayer);
            if (istouchground) return;
        }
    }
    void AirStateChange()
    {
        if (istouchground)
        {
            anim.SetBool("falltofloat", true);
            anim.SetBool("floattojump", false);
            anim.SetBool("jumptofall", false);
            anim.SetBool("floattofall", false);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("floattojump", true);
            anim.SetBool("falltofloat", false);
            anim.SetBool("jumptofall", false);
            anim.SetBool("floattofall", false);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumptofall", true);
            anim.SetBool("floattofall", true);
            anim.SetBool("falltofloat", false);
            anim.SetBool("floattojump", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rightentertag")
        {

            SceneManager.LoadScene("Scene_2");
        }
        if (collision.tag == "leftentertag")
        {

            SceneManager.LoadScene("Scene_1");
        }
    }
}
