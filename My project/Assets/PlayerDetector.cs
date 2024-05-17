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

            if (!IsPlayerHiding(collision.transform))
            {
                Debug.Log("Player not hiding. Triggering game over."); // 플레이어 감지 및 숨지 않음 확인
                animator.SetTrigger("EatPlayer");
                GameManager2.Instance.GameOver();
            }
        }
    }

    bool IsPlayerHiding(Transform player)
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, Vector2.Distance(transform.position, player.position), hidingLayerMask);

        // 디버그 드로잉 추가
        Debug.DrawRay(transform.position, directionToPlayer * Vector2.Distance(transform.position, player.position), Color.red, 1.0f);

        return hit.collider != null;
    }
}
