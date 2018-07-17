using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    [SerializeField]
    private Text showLines;
    [SerializeField]
    private Text lifes;
    [SerializeField]
    private GameObject linesContainer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowLinesContainer()
    {
        linesContainer.SetActive(!linesContainer.activeSelf);
    }

    #region Gets And Sets for UI
    public Text ShowLines
    {
        get
        {
            return showLines;
        }

        set
        {
            showLines = value;
        }
    }

    public Text Life
    {
        get
        {
            return lifes;
        }

        set
        {
            lifes = value;
        }
    }
    #endregion

}
