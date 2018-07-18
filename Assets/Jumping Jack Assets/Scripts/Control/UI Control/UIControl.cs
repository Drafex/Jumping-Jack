using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    #region Score Parameters
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text highScore;
    #endregion

    #region Life Parameters
    [SerializeField]
    private Text lifes;
    #endregion

    #region Message Parameters
    [SerializeField]
    private Text showLines;
    [SerializeField]
    private GameObject linesContainer;
    [SerializeField]
    private GameObject gameOver;
    #endregion

    #region Unity Functions
    void Start()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("High Score");
    }
    #endregion

    #region Message Function
    public void ShowLinesContainer()
    {
        linesContainer.SetActive(!linesContainer.activeSelf);
    }
    #endregion

    #region Levels Function
    public void GameOver()
    {
        gameOver.SetActive(!gameOver.activeSelf);
    }
    #endregion

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
