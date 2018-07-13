using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {
    #region Parameters For Movement
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int jumpsAllow;
    private int jumps;
    private Rigidbody2D rb;
    #endregion

    #region Unity Functions
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumps = 0;
        }
    }
    #endregion

    #region Movement Functions
    public void Move(float direction)
    {
        transform.Translate(new Vector2(direction, 0f) * speed);
    }

    public void Jump()
    {
        if (jumps < jumpsAllow)
        {
            rb.AddForce(new Vector2(0f, 1f) * jumpForce);
            jumps++;
        }
    }
    #endregion

    #region Gets And Sets For Movement
    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
    }
    #endregion
}
