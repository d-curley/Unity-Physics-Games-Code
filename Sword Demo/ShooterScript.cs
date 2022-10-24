using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    float forceAmp = -150;
    [SerializeField] Rigidbody bullet;
    protected float bulletTimer;
    public bool active;
    private bool dead = false;

    void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > 5)
        {
            bulletTimer = Random.Range(0f,1f);

            float aim = Random.Range(-5f, 5f) + Random.Range(-5f, 5f) + Random.Range(-5f, 5f) + Random.Range(-5f, 5f);

            Rigidbody proj = Instantiate(bullet, new Vector3(transform.position.x*(.9f), transform.position.y, transform.position.z - 3), Quaternion.identity); ;
            proj.AddForce(new Vector3(transform.position.x*(forceAmp/25f)+aim,0,forceAmp));
        }
    }
    public void OnCollisionEnter(Collision myCol)
    {
        if (myCol.gameObject.name == "Projectile(Clone)")
        {
            Destroy(gameObject);
            SendMessageUpwards("ShooterHit",gameObject.transform.position.x);
        }
    }

    void PlayerDead() 
    { Destroy(gameObject); }

    void ResetGame()
    {
        Destroy(gameObject);
        bulletTimer = 0;
    }

}
