using UnityEngine;

public class Winner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool ReachEnd = GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"));

        if (ReachEnd && (other.GetType() == typeof(BoxCollider2D)))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Winner();
        }
    }
}
