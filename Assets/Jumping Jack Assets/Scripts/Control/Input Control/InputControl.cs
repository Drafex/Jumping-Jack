using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

    #region Unity Functions

    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
        {
            GameControl.instance.Character.Move(Input.GetAxis("Horizontal"));
        }
        else
        {
            if (GameControl.instance.Character.Animator.gameObject.activeSelf)
            {
                if (GameControl.instance.Character.Animator.GetInteger("State") == (int)EAnima.run)
                {
                    GameControl.instance.Character.Animator.SetInteger("State", (int)EAnima.idle);
                }
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            GameControl.instance.Character.Jump();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    #endregion

}
