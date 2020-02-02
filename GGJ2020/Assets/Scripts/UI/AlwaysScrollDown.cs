using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlwaysScrollDown : MonoBehaviour
{
    private ScrollRect scrollRect;

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
}
