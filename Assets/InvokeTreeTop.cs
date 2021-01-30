using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeTreeTop : MonoBehaviour
{
    public CameraEndingMovement cm;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collision start seq");
            cm.StartEndingSequence();
        }
    }
}
