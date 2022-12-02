using UnityEngine;

public class EnemyDamageCollisionData : MonoBehaviour
{

    void OnCollisionStay(Collision other)
    {
        if (other.collider.transform.CompareTag("Player"))
        {
            other.transform.SendMessage("ApplyDamage", 1);
        }
    }
}
