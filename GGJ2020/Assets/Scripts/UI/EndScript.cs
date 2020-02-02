using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class EndScript : MonoBehaviour
{
    public float uiActivateDelay = 5f;
    public string[] endAnimations = new string[2];
    public string[] endMessages = new string[2];
    public Text endMessage;
    public Animator animator;
    public GameObject uiRoot;
    public GameObject endCanvas;
    private ControlsMaster control;

    public void RunEndscreen(string animation)
    {
        int index = -1;
        for(int i = 0; i < endAnimations.Length; i++)
        {
            if(endAnimations[i].Equals(animation))
            {
                index = i;
                break;
            }
        }

        if(index >= 0)
        {
            if(endCanvas != null)
            {
                endCanvas.SetActive(true);
            }

            if(endMessage != null)
            {
                endMessage.text = endMessages[index];
            }

            if(animator != null)
            {
                animator.SetBool(animation, true);
            }

            StartCoroutine("UITimer", uiActivateDelay);

            if(uiRoot != null)
            {
                uiRoot.SetActive(false);
            }
        }
    }

    private IEnumerator UITimer(float time)
    {
        yield return new WaitForSeconds(time);
        uiRoot.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        if (control == null)
        {
            control = new ControlsMaster();
            control.Player.UIAccept.performed += UIAccept;
            control.Player.UIDecline.performed += UIDecline;
        }
        control.Player.Enable();
    }

    private void UIDecline(InputAction.CallbackContext obj)
    {
        Exit();
    }

    private void UIAccept(InputAction.CallbackContext obj)
    {
        Restart();
    }

    private void OnDisable()
    {
        control.Player.Disable();
    }

    void Update()
    {
        
    }
}
