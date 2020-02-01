using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceTrigger : MonoBehaviour
{
    [SerializeField] private bool destroyAfterUse = true;

    private Activatable activatable;

    private void Start()
    {
        activatable = gameObject.GetComponent<Activatable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            activatable.Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            activatable.Deactivate();
            if(destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
