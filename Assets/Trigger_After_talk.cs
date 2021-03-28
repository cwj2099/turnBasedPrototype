using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_After_talk : MonoBehaviour
{
    CameraController cam;
    [SerializeField]
    GameObject locator;
    dialogueManager diaM;
    [SerializeField]
    UnityEvent myEvent;
    bool ready;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        diaM = FindObjectOfType<dialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.locators[cam.location]==locator)
        {
            if (diaM.anim.GetBool("isOpen"))
            {
                ready = true;
            }
            else
            {
                if (ready)
                {
                    ready = false;
                    myEvent.Invoke();
                }
            }
        }

    }
}
