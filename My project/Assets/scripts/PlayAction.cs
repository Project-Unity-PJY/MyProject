using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAction : MonoBehaviour
{
    public float Speed; // 플레이어의 이동 속도
    public GameManager manager; // 게임 매니저 참조
    public float runSpeedMultiplier = 2f; // 뛰는 속도의 배수
    private bool isRunning = false; // 뛰고 있는지 여부
    Rigidbody2D rigid; // 플레이어의 Rigidbody2D 컴포넌트
    Animator anim; // 애니메이터 컴포넌트
    float h; // 수평 입력 값
    float v; // 수직 입력 값
    bool isHorizonMove; // 수평 이동 여부
    Vector3 dirVec; // 플레이어가 바라보는 방향
    GameObject scanObject; // 스캔할 객체


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        anim = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        // 플레이어의 입력 관리
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // 키 입력 상태 확인
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // 수평 이동 결정
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // Shift 키를 누를 때 속도 증가
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // 애니메이션 업데이트
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // 뛰는 상태일 때 애니메이션 파라미터 설정
        anim.SetBool("isRunning", isRunning);



        // 플레이어가 바라보는 방향 설정
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        // 'Jump' 버튼을 누를 때 객체 스캔
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);
    }

    void FixedUpdate()
    {
        // 플레이어 이동 처리
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        Vector2 rayStart = rigid.position + new Vector2(0, 0.5f);
        float currentSpeed = isRunning ? Speed * runSpeedMultiplier : Speed;
        rigid.velocity = moveVec * currentSpeed;

        // 레이캐스트를 사용하여 객체 탐지
        Debug.DrawRay(rayStart, dirVec * 1.0f, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rayStart, dirVec, 0.7f, LayerMask.GetMask("Object"));

        // 레이캐스트가 객체와 충돌하면 해당 객체를 저장
        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}
