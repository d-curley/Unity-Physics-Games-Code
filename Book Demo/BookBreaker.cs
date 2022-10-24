using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookBreaker : MonoBehaviour
{
    public int fails=0;
    public void OnCollisionEnter(Collision myCol)
    {
        if (myCol.gameObject.name == "Book(Clone)")
        {
            Destroy(myCol.gameObject);
            fails++;
        }
    }

}
