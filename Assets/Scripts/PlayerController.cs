using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class PlayerController : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
    }
}
