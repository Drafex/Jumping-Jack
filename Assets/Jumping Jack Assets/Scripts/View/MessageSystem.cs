using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour {

    #region Parameters For Writing
    private MessageData messageData;
    #endregion

    #region Unity Functions
    void Start()
    {
        messageData = GetComponent<MessageData>();
    }
    #endregion

    #region Write Message
    public void NextLine(string line)
    {
        StartCoroutine(ShowLine(line));
    }

    private IEnumerator ShowLine(string nextLine)
    {
        GameControl.instance.UIControl.ShowLines.text = "";
        yield return new WaitForSeconds(0.5f);
        GameControl.instance.WritingSound.Play();
        foreach (char item in nextLine)
        {
            GameControl.instance.UIControl.ShowLines.text += item;
            yield return new WaitForSeconds(0.2f);
        }
        GameControl.instance.WritingSound.Stop();
        yield return new WaitForSeconds(4f);
        GameControl.instance.ResetLevel();
    }
    #endregion

    #region Gets for Message
    public MessageData MessageData
    {
        get
        {
            return messageData;
        }
    }
    #endregion

}
