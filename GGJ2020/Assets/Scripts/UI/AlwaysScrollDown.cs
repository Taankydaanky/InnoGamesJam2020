using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlwaysScrollDówn : MonoBehaviour
{
    public Text messageField;
    private ScrollRect scrollRect;
    private bool firstMessage = true;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void AppendText(string text)
    {
        if(messageField != null)
        {
            if(firstMessage)
            {
                firstMessage = !firstMessage;
            }
            else
            {
                messageField.text += "\n----------------\n";
            }
            messageField.text += text.Replace("\\n", "\n");
        }
    }

    public void Clear()
    {
        if(messageField != null)
        {
            messageField.text = "";
        }

        firstMessage = true;
    }
}
