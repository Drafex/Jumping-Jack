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
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text highScore;
    // Use this for initialization
    void Start () {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("High Score");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowLinesContainer()
    {
        linesContainer.SetActive(!linesContainer.activeSelf);
    }

    public void GameOver()
    {
        gameOver.SetActive(!gameOver.activeSelf);
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

    public Text Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public Text HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }
    #endregion

}
