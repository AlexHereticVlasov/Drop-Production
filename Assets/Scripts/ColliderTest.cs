using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("C");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("T"); 
    }
}
