using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWhenEnter : MonoBehaviour
{
    public UnityEvent myEvent;
    public GameObject myLocator;
    public bool repeatable;
    public int key;
    bool cd = false;
    Messenger mes;
    CameraController cam;
    // Start is called before the first frame update
    void Start()
    {

        mes = FindObjectOfType<Messenger>();
        cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.locators[cam.location]== myLocator && cam.changeCounter <= 0.01)
        {

            if (!cd)
            {
                cd = true;
                trigger();
            }
        }
        else
        {
            cd = false;
        }
    }

    void trigger()
    {
        
        if (!repeatable)
        {
            if (!mes.EventTriggered[key])
            {
                mes.EventTriggered[key] = true;
                myEvent.Invoke();
            }

        }
        else
        {
            myEvent.Invoke();
        }
    }
}
