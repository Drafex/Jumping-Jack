using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageData : MonoBehaviour {
    [SerializeField]
    private List<string> lines = new List<string>();
    [SerializeField]
    private string gameOverLine;

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
