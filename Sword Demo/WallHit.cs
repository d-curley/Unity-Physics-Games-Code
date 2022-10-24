using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallHit : MonoBehaviour
{


    public void OnCollisionEnter(Collision myCol)
    {
        if (myCol.gameObject.name == "Projectile(Clone)")
        {
            Destroy(myCol.gameObject);
        }
    }
}
