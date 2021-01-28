using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed = 1f;


    private Vector2 desiredMovement = Vector2.zero;
    private float speedModifier = 1f;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
            Debug.LogError("No Rigidbody2D found");
    }

    void FixedUpdate()
    {
        // smooth axis for now, can change to GetAxisRaw
        desiredMovement = new Vector2(Input.GetAxis("Horizontal"), speed * speedModifier);

        rb2d.MovePosition(rb2d.position + desiredMovement * Time.fixedDeltaTime);
    }
}
