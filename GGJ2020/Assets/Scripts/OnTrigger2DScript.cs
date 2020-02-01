using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OnTrigger2DScript : MonoBehaviour
{
    public AudioMixerSnapshot lounge;
    public AudioMixerSnapshot nonLounge;
    public float transitionTime = .8f;

    void OnTriggerEnter2D()
    {
        lounge.TransitionTo(transitionTime);
    }
    void OnTriggerExit2D()
    {
        nonLounge.TransitionTo(transitionTime);
    }
}
