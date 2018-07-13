using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    #region References
    private Character character;
    private UIControl uiControl;
    private InputControl inputControl;
    public static GameControl instance;
    #endregion

    #region Prefabs
    [SerializeField]
    private GameObject prfCharacter;
    #endregion

    #region Unity Fucntions
    void Start()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;

        character = Instantiate(prfCharacter).GetComponent<Character>();

        uiControl = gameObject.AddComponent<UIControl>();

        inputControl = gameObject.AddComponent<InputControl>();
    }
    #endregion

    #region Gets For References
    public Character Character
    {
        get
        {
            return character;
        }
    }

    public UIControl UIControl
    {
        get
        {
            return UIControl;
        }
    }

    public InputControl InputControl
    {
        get
        {
            return inputControl;
        }
    }
    #endregion
}
