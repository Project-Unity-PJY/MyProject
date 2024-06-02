using UnityEngine;

public class NPC : MonoBehaviour
{
    public int npcID; // NPC ID

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SetCurrentNPC(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SetCurrentNPC(null);
        }
    }
}
