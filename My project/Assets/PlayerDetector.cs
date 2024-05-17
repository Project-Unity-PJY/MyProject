using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Animator animator; // 몬스터의 애니메이터
    public LayerMask hidingLayerMask; // 나무 오브젝트가 있는 레이어

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.collider.name); // 충돌 감지 확인

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player detected"); // 플레이어 감지 확인
            GameManager2.Instance.setEnd();
            animator.SetTrigger("eat");

            StartCoroutine(WaitForEatAnimation());
        }
    }

    private IEnumerator WaitForEatAnimation()
    {
        int time = 0;
        // 애니메이션의 "eat" 상태가 끝날 때까지 기다림
        while (time < 5)
        {
            time++;
            yield return new WaitForSeconds(1.0f);
        }

        GameManager2.Instance.GameOver();
    }
}
