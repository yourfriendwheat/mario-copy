using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool HeadTouchPlayer = GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"));

        if (HeadTouchPlayer && (other.GetType() == typeof(BoxCollider2D)))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(300);
            Destroy(transform.parent.gameObject);
        }
    }

}
