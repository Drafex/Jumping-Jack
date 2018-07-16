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
    private bool isJumping;
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
            if (isJumping)
            {
                print("one life lost");
                isJumping = false;
            }
            jumps = 0;
        }

        if (collision.gameObject.tag == "Hole")
        {
            if (isJumping)
            {
                if (moveUp != null)
                {
                    StopCoroutine(moveUp);
                }
                else
                {
                    moveUp = StartCoroutine(MoveUP(new Vector2(transform.position.x,
                    collision.gameObject.GetComponent<SpriteMask>().bounds.size.y +
                    collision.transform.position.y + 0.1f)));
                    GameControl.instance.CreateHoleOrMonster(0);
                }
                isJumping = false;
            }
            else
            {
                if (moveUp != null)
                {
                    StopCoroutine(moveUp);
                }
                else
                {
                    moveUp = StartCoroutine(MoveUP(new Vector2(transform.position.x,
                    collision.transform.position.y -
                    collision.gameObject.GetComponent<SpriteMask>().bounds.size.y - 0.1f)));
                }
            }
        }

        if (collision.gameObject.tag == "Monster")
        {
            //paralice
        }
    }
    #endregion

    #region Movement Functions
    public void Move(float direction)
    {
        transform.Translate(new Vector2(direction, 0f) * speed);
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0 && direction < 0)
        {
           
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,
                transform.position.y);
        }
        else if (Camera.main.WorldToViewportPoint(transform.position).x > 1 && direction > 0)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0f, 0)).x,
                transform.position.y);
        }
    }

    public void Jump()
    {
        if (jumps < jumpsAllow)
        {
            rb.AddForce(new Vector2(0f, 1f) * jumpForce);
            isJumping = true;
            jumps++;
        }
    }

    private IEnumerator MoveUP(Vector2 position)
    {
        float startTime = Time.time;
        float lerpTime = 0.5f;
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
