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
    //record scene id
    public int now_sceneid;
    //capability_dash
    public static bool CanDash = false;
    public float dashspeed;
    public float dashtime;
    bool isdashing;
    float startdashtime;
    public GameObject shadow;
    public int shadownum;
    float shadowtime;
    //left and right wall check
    public bool withwall;
    public Transform wallcheck;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(tf);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        now_sceneid = SceneManager.GetActiveScene().buildIndex;
        if (now_sceneid == 0) SceneManager.LoadScene("Scene_1");

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        istouchground = true;

        shadowtime = dashtime;
    }

    void Update()
    {
        if (!isdashing)
        {
            HorizontalMove();
            Jump();
            AirStateChange();
            if (CanDash) Dash();
        }
        else
        {
            Dashing();
        }
    }
    void HorizontalMove() 
    {
        CheckWall();
        float direction = Input.GetAxis("Horizontal");
        if (direction < 0)
        {
            tf.eulerAngles = new Vector3(tf.rotation.x, 180, tf.rotation.z);
        }
        else if (direction > 0)
        {
            tf.eulerAngles = new Vector3(tf.rotation.x, 0, tf.rotation.z);
        }
        if (!withwall)
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
    void CheckWall()
    {
        withwall = Physics2D.Linecast(transform.position, wallcheck.position, GroundLayer);
    }
    void CheckOnGround() {
        for (int i = 0; i < 3; i++) {
            istouchground = Physics2D.Linecast(transform.position, GroundChecks[i].position, GroundLayer);
            if (istouchground) return;
        }
    }
    void AirStateChange()//change ghost's animation
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
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)//change scenes event
    {
        if (now_sceneid == 0 && arg1.name == "Scene_1")
        {
            tf.position = new Vector3(0f, 0f, 0f);
        }
        else if (now_sceneid == 1 && arg1.name == "Scene_2")
        {
            tf.position = new Vector3(-8f, -1.5f, 0f);
        }
        else if (now_sceneid == 2 && arg1.name == "Scene_1")
        {
            tf.position = new Vector3(14.5f, -0.64f, 0f);
        }
        now_sceneid = SceneManager.GetActiveScene().buildIndex;
    }
    void Dash() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startdashtime = dashtime;
            isdashing = true;
        }
    }
    void Dashing()
    {
        startdashtime -= Time.deltaTime;
        if(startdashtime < 0)
        {
            isdashing = false;
            startdashtime = dashtime;
            shadowtime = dashtime;
        }
        else
        {
            rb.velocity = tf.right * dashspeed;
            if (startdashtime < shadowtime)
            {
                shadowtime -= dashtime / shadownum;
                if (Input.GetAxis("Horizontal") < 0)
                    Instantiate(shadow, tf.position, new Quaternion(0, 180, 0, 0));
                else
                    Instantiate(shadow, tf.position, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
