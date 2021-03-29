using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    public static GameObject selected;
    public CameraController Cam;
    public UnityEvent myEvent;
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(selected);
        if (Input.GetKeyDown(KeyCode.Space)) { GetClicked(); }
    }

    public virtual void GetClicked()
    {
        myEvent.Invoke();
    }
}
