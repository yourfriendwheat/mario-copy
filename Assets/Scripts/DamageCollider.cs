using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool Bodytouchplayer = GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"));

        if(Bodytouchplayer && (other.GetType() == typeof(CapsuleCollider2D)))
        {
            GameManager gameManager = other.GetComponent<GameManager>();

            GameManager.Instance.ProcessPlayerDeath();
        }
    }

}
