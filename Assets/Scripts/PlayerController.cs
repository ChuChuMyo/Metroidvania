using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class PlayerController : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    float speed = 5f;
    float h;
    bool isJump,isCroush = false;
    Rigidbody2D rbody;
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
        h = Input.GetAxis("Horizontal");
        if(!isCroush) // 앉기 상태가 아닐 경우에만 이동 가능
        {
            rbody.velocity = new Vector2(h * speed, rbody.velocity.y);
        }
        if (h > 0) //오른쪽으로 이동시 오른쪽 바라봄
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("IsRun", true);
        }
        else if (h < 0) //왼쪽으로 이동시 왼쪽 바라봄
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("IsRun", true);
        }
        if (h == 0) //이동이 아닐경우 아이들 모션
        {
            anim.SetBool("IsRun", false);
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.S)) //S키를 누르면 앉기
        {
            anim.SetBool("IsCroush", true);
            rbody.velocity = Vector2.zero; // 앉으면 가속도 초기화
            isCroush = true;
        }
        if(Input.GetKeyUp(KeyCode.S)) //S키를 떼면 일어서기
        {
            anim.SetBool("IsCroush", false);
            isCroush = false;
        }
        if(Input.GetButtonDown("Jump") && !isJump) //스페이스바를 누르고 점프중이 아니면 점프
        {
            anim.SetBool("IsJump", true);
            //rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            rbody.AddForce(new Vector2(0,350));
            isJump = true;
        }
        if (rbody.velocity.y < -0.1f) //캐릭터가 떨어지는 중이면 떨어지는 애니메이션 작동
        {
            anim.SetBool("IsFall", true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground") //땅에 착지시 점프 가능, 점프모션, 추락모션 끄기
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFall", false);
            isJump = false;
        }
    }
}
