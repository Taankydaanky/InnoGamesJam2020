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
    public AudioMixerSnapshot colliding;
    public AudioMixerSnapshot stillColliding;
    public AudioMixerSnapshot nonColliding;

    public float transitionTime = .8f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding.TransitionTo(transitionTime);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stillColliding.TransitionTo(transitionTime);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nonColliding.TransitionTo(transitionTime);
        }
    }

}
