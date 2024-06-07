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
    private bool currentNPC; // 현재 대화 중인 NPC

    // 레이캐스트를 쏘는 최대 거리
    public float rayDistance = 0.1f;
    // 플레이어 오브젝트
    public Transform player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        currentNPC = false;
    }

    void Update()
    {   
        // 대화중 아닐 때만 이동 가능
        if (!currentNPC) {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            transform.position = transform.position + new Vector3(moveX, 0, 0);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // 점프 힘을 추가
                isGrounded = false; // 점프한 후에는 공중에 있으므로 isGrounded를 false로 설정
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (isTouchingTree)
                {
                    gameObject.layer = LayerMask.NameToLayer("HiddenPlayer"); // 플레이어를 숨김 레이어로 변경
                }

                if (isTouchingVine)
                {
                    animator.SetBool("isClimbing", true); // 덩굴 오르는 애니메이션 활성화
                    if (transform.position.y > vineBottom)
                    {
                        transform.Translate(new Vector3(0, -(climbSpeed * Time.deltaTime), 0)); // 내려가기
                    }
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (isTouchingVine)
                {
                    animator.SetBool("isClimbing", true); // 덩굴 오르는 애니메이션 활성화
                    if (transform.position.y < vineTop)
                    {
                        transform.Translate(new Vector3(0, climbSpeed * Time.deltaTime, 0)); // 올라가기
                    }
                }
            }
        }

        // 스페이스바 입력을 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentNPC)
            {
                GameManager2.Instance.talkManager3.nextTalk();
            }
            else
            {
                // 플레이어 오브젝트의 위치에서 레이캐스트 발사
                Vector2 origin = player.position;

                RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, 0.1f);

                // 레이캐스트가 충돌한 경우
                if (hit.collider != null)
                {
                    // 충돌한 물체에서 TalkData 컴포넌트 찾기
                    TalkData talkData = hit.collider.GetComponent<TalkData>();

                    if (talkData != null)
                    {
                        GameManager2.Instance.talkManager3.setTalk(talkData);
                        currentNPC = true;
                    }
                    else
                    {
                        // TalkData 컴포넌트가 없는 경우의 로직
                        Debug.Log("TalkData 컴포넌트가 없습니다: " + hit.collider.name);
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true; // 땅에 닿으면 isGrounded를 true로 설정
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hidingLayerMask")
        {
            isTouchingTree = true; // 나무와 접촉 중임을 표시
        }

        if (collision.tag == "Vine")
        {
            isTouchingVine = true; // 덩굴과 접촉 중임을 표시
            vineTop = collision.bounds.max.y;
            vineBottom = collision.bounds.min.y;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "hidingLayerMask")
        {
            isTouchingTree = false; // 나무와 접촉 종료
            gameObject.layer = LayerMask.NameToLayer("Default"); // 플레이어 레이어를 기본값으로 변경
        }

        if (collision.tag == "Vine")
        {
            isTouchingVine = false; // 덩굴과 접촉 종료
            animator.SetBool("isClimbing", false); // 덩굴 오르는 애니메이션 비활성화
        }
    }

    public void SetCurrentNPC(bool isEnd)
    {
        currentNPC = isEnd;
    }
}
