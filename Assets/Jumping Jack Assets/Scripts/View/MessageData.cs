using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageData : MonoBehaviour {

    #region Message Data Parameters
    [SerializeField]
    private List<string> lines = new List<string>();
    [SerializeField]
    private string gameOverLine;
    #endregion

    #region Gets for Lines
    public List<string> Lines
    {
        get
        {
            return lines;
        }
    }

    public string GameOverLine
    {
        get
        {
            return gameOverLine;
        }
    }
    #endregion
}
