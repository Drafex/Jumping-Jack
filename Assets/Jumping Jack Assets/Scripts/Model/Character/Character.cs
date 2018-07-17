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
    private float paralizeTime;
    [SerializeField]
    private int jumpsAllow;
    private int jumps;
    private int lifes;
    private bool paralized;
    private Rigidbody2D rb;
    private Coroutine moveUp;
    private Coroutine paralize;
    private Coroutine letItFall;
    #endregion

    #region Unity Functions
    void Start()
    {
        lifes = 6;
        GameControl.instance.UIControl.Life.text = "Lifes: " + lifes;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (collision.transform.position.y > transform.position.y)
            {
                if (paralize != null)
                {
                    StopCoroutine(paralize);
                }
                paralize = StartCoroutine(Paralize());
            }
            if (!GameControl.instance.Floors.Contains(collision.transform) && paralized)
            {
                lifes--;
                GameControl.instance.UIControl.Life.text = "Lifes: " + lifes;
                if (lifes <= 0)
                {
                    if (PlayerPrefs.GetInt("High Score") < GameControl.instance.Score)
                    {
                        PlayerPrefs.SetInt("High Score", GameControl.instance.Score);
                    }
                    PlayerPrefs.SetInt("Higher Score", GameControl.instance.Score);
                    GameControl.instance.UIControl.ShowLinesContainer();
                    GameControl.instance.MessageSystem.NextLine(GameControl.instance.MessageSystem.MessageData
                        .GameOverLine);
                    GameControl.instance.UIControl.GameOver();
                }
            }
            jumps = 0;
        }

        if (collision.gameObject.tag == "Hole")
        {
            if (collision.transform.position.y > transform.position.y)
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
                    if (collision.transform.position.y >= GameControl.instance.Floors[
                        GameControl.instance.Floors.Count-1].position.y)
                    {
                        GameControl.instance.NextLevel();
                    }
                    GameControl.instance.CreateHoleOrMonster(0);
                    GameControl.instance.Score += GameControl.instance.ScoreAdded;
                    GameControl.instance.UIControl.Score.text = "Score: " + GameControl.instance.Score;
                }
            }
            else
            {
                if (letItFall != null)
                {
                    StopCoroutine(letItFall);
                }
                else
                {
                    letItFall = StartCoroutine(LetItFall(collision.transform.position.y - 
                        collision.gameObject.GetComponent<SpriteMask>().bounds.size.y));
                    if (paralize != null)
                    {
                        StopCoroutine(paralize);
                    }
                    paralize = StartCoroutine(Paralize());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            if (paralize != null)
            {
                StopCoroutine(paralize);
            }
            paralize = StartCoroutine(Paralize());
        }
    }
    #endregion

    #region Movement Functions
    public void Move(float direction)
    {
        if (!paralized)
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
    }

    public void Jump()
    {
        if (jumps < jumpsAllow && !paralized)
        {
            rb.AddForce(new Vector2(0f, 1f) * jumpForce);
            jumps++;
        }
    }

    private IEnumerator MoveUP(Vector2 position)
    {
        paralized = true;
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
        paralized = false;
    }

    private IEnumerator LetItFall(float posY)
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        while (transform.position.y >= posY)
        {
            yield return new WaitForEndOfFrame();
        }
        GetComponent<BoxCollider2D>().isTrigger = false;
        letItFall = null;
    }

    private IEnumerator Paralize()
    {
        paralized = true;
        yield return new WaitForSeconds(paralizeTime);
        paralized = false;
        paralize = null;
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

    public bool Paralized
    {
        get
        {
            return paralized;
        }

        set
        {
            paralized = value;
        }
    }
    #endregion
}
