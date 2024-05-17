using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float jumpForce = 5f; // 점프력

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트를 저장할 변수
    private bool isGrounded; // 땅에 닿아 있는지 여부를 확인하는 변수

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
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
    }

    // 땅에 닿았는지 여부 확인하기 위한 간단한 방법
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
