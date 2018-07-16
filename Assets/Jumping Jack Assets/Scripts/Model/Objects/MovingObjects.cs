using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private int indexOnFloor;
    private Vector2 direction; 
    #region Unity Functions
    void Start()
    {
        StartCoroutine(Moving());
    }
    #endregion

    #region Movement Functions
    private IEnumerator Moving()
    {
        while (true)
        {
            transform.Translate(direction * speed);
            if (Camera.main.WorldToViewportPoint(transform.position).x < -0.05 && direction.x < 0)
            {
                indexOnFloor++;
                if (indexOnFloor > 7)
                {
                    indexOnFloor = 0;
                }
                transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1.055f, 0)).x,
                    GameControl.instance.Floors[indexOnFloor].position.y);
            }
            else if (Camera.main.WorldToViewportPoint(transform.position).x > 1.05 && direction.x > 0)
            {
                indexOnFloor--;
                if (indexOnFloor < 0)
                {
                    indexOnFloor = 7;
                }
                transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(-0.055f, 0)).x,
                    GameControl.instance.Floors[indexOnFloor].position.y);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion

    #region Gets and Sets For Movement
    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public int IndexOnFloor
    {
        get
        {
            return indexOnFloor;
        }

        set
        {
            indexOnFloor = value;
        }
    }
    #endregion


}
