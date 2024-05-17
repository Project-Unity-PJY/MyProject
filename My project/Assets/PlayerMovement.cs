using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float jumpForce = 5f; // 점프력
    public float climbSpeed = 2f; // 플레이어가 덩굴을 올라가는 속도

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트를 저장할 변수
    private Animator animator;
    private bool isGrounded; // 땅에 닿아 있는지 여부를 확인하는 변수
    private bool isTouchingTree = false;
    private bool isTouchingVine = false;
    private float vineTop; // 덩굴의 상단 경계
    private float vineBottom; // 덩굴의 하단 경계

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌우 이동
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveX, 0, 0);

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // 점프한 후에는 공중에 있으므로 isGrounded를 false로 설정
        }

        // 숨기 or 내려가기
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (isTouchingTree)
            {
                gameObject.layer = LayerMask.NameToLayer("HiddenPlayer");
            }

            if (isTouchingVine)
            {
                animator.SetBool("isClimbing", true);
                // 플레이어가 덩굴의 하단 경계를 넘지 않도록 함
                if (transform.position.y > vineBottom)
                {
                    transform.Translate(new Vector3(0, -(climbSpeed * Time.deltaTime), 0));
                }
            }
        }

        // 올라가기
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isTouchingVine)
            {
                animator.SetBool("isClimbing", true);
                // 플레이어가 덩굴의 상단 경계를 넘지 않도록 함
                if (transform.position.y < vineTop)
                {
                    transform.Translate(new Vector3(0, climbSpeed * Time.deltaTime, 0));
                }
            }
        }
    }

    // 땅에 닿았는지 여부 확인하기 위한 간단한 방법
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 나무 충돌 감지
        if (collision.tag == "hidingLayerMask")
        {
            isTouchingTree = true;
        }

        // 덩굴 충돌 감지
        if (collision.tag == "Vine")
        {
            isTouchingVine = true;
            // 덩굴의 경계를 저장
            vineTop = collision.bounds.max.y;
            vineBottom = collision.bounds.min.y;
        }
    }

    // 충돌 끝났는지 확인
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 나무 충돌 끝나는거 감지
        if (collision.tag == "hidingLayerMask")
        {
            isTouchingTree = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        // 덩굴 충돌 끝나는거 감지
        if (collision.tag == "Vine")
        {
            isTouchingVine = false;
            animator.SetBool("isClimbing", false);
        }
    }
}
