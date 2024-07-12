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
        if(!isCroush) // �ɱ� ���°� �ƴ� ��쿡�� �̵� ����
        {
            rbody.velocity = new Vector2(h * speed, rbody.velocity.y);
        }
        if (h > 0) //���������� �̵��� ������ �ٶ�
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("IsRun", true);
        }
        else if (h < 0) //�������� �̵��� ���� �ٶ�
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("IsRun", true);
        }
        if (h == 0) //�̵��� �ƴҰ�� ���̵� ���
        {
            anim.SetBool("IsRun", false);
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.S)) //SŰ�� ������ �ɱ�
        {
            anim.SetBool("IsCroush", true);
            rbody.velocity = Vector2.zero; // ������ ���ӵ� �ʱ�ȭ
            isCroush = true;
        }
        if(Input.GetKeyUp(KeyCode.S)) //SŰ�� ���� �Ͼ��
        {
            anim.SetBool("IsCroush", false);
            isCroush = false;
        }
        if(Input.GetButtonDown("Jump") && !isJump) //�����̽��ٸ� ������ �������� �ƴϸ� ����
        {
            anim.SetBool("IsJump", true);
            //rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            rbody.AddForce(new Vector2(0,350));
            isJump = true;
        }
        if (rbody.velocity.y < -0.1f) //ĳ���Ͱ� �������� ���̸� �������� �ִϸ��̼� �۵�
        {
            anim.SetBool("IsFall", true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground") //���� ������ ���� ����, �������, �߶���� ����
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFall", false);
            isJump = false;
        }
    }
}
