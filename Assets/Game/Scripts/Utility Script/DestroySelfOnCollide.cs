using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
