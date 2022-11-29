
using UnityEngine;

public class FiringBulletState : MonoBehaviour
{
    public GameObject projectile;
    public float bulletTimer = 2.0f;
    public int maxBullets = 7;
    public int Bullets = 7;

    public int cooldown = 0;

    private GameObject clone;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Bullets != 0 )
        {
            Debug.Log("Bullets: " + Bullets);
            clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);

            //destroy bullet after 2sec
            Destroy(clone, bulletTimer);
            Bullets -= 1;
        }

        if(cooldown > 3){

            cooldown = 3;

        }

        if (Input.GetKeyDown(KeyCode.R) || Bullets < 7 && cooldown == 0)
        {
            Debug.Log("Reloading: " + cooldown + " & Bullet: " + Bullets);
            Bullets = maxBullets;

            
        }

    }
}

