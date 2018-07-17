using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour {

    private MessageData messageData;

    [SerializeField]

	void Start () {
        messageData = GetComponent<MessageData>();
	}

    public void NextLine(string line)
    {
        StartCoroutine(ShowLine(line));
    }

    private IEnumerator ShowLine(string nextLine)
    {
        GameControl.instance.UIControl.ShowLines.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char item in nextLine)
        {
            GameControl.instance.UIControl.ShowLines.text += item;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(4f);
        GameControl.instance.ResetLevel();
    }

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
