using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookScore : MonoBehaviour
{
    public int Score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Book(Clone)")
        {
            Score++;
            Debug.Log(Score);
        }
    }

}
