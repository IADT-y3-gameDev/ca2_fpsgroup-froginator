using UnityEngine;

public class EnemyDamagedData : MonoBehaviour
{
    private int bulletHits;

    private void  OnEnable() {
        bulletHits = 0;
    }
    void OnCollisionEnter(Collision other)
    {   
        Debug.Log("Collision");
        
        if (other.collider.transform.CompareTag("bullet"))
        {
            bulletHits++;
            Debug.Log(bulletHits);
        }
        if (bulletHits == 3)
        {
            gameObject.SetActive(false); 
            //Destroy(gameObject);
        }
    }
}