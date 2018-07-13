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
    private Coroutine moveUp;
    #endregion

    #region Unity Functions
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumps = 0;
        }

        if (collision.gameObject.GetComponent<Hole>())
        {
            if (moveUp != null)
            {
                StopCoroutine(moveUp);
            }
            else
            {
                moveUp = StartCoroutine(MoveUP(new Vector2(transform.position.x,
                collision.gameObject.GetComponent<SpriteMask>().bounds.size.y +
                collision.transform.position.y)));
            }
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

    private IEnumerator MoveUP(Vector2 position)
    {
        float startTime = Time.time;
        float lerpTime = 1f;
        float endTime = startTime + lerpTime;
        float t;
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.simulated = false;
        while (Time.time <= endTime)
        {
            t = (Time.time - startTime) / lerpTime;
            transform.position = Vector3.Lerp(transform.position, position, t);
            yield return new WaitForEndOfFrame();
        }
        GetComponent<BoxCollider2D>().isTrigger = false;
        rb.simulated = true;
        moveUp = null;
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
