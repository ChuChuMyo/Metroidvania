using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class PlayerController : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public float speed;
    bool isJump = false;
    Rigidbody2D rbody;
    float newVel, oldVel;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnKeyboard();
        PlayerMove();
    }
    
    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("IsRun", true);
        }
        else if (h < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("IsRun", true);
        }
        if (h == 0)
        {
            anim.SetBool("IsRun", false);
        }
        transform.Translate(h * speed * Time.deltaTime, 0, 0);
    }

    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("IsCroush", true);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("IsCroush", false);
        }
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            anim.SetBool("IsJump", true);
            rbody.AddForce(Vector2.up*250);
            isJump = true;
        }
        if (rbody.velocity.y < -0.01f)
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFall", true);
        }
        
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFall", false);
            isJump = false;
        }
    }
}
