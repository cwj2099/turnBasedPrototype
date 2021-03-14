using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public static GameObject selected;
    public CameraController Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(selected);
        if (Input.GetKeyDown(KeyCode.J)) { GetClicked(); }
    }

    public virtual void GetClicked()
    {
        selected = this.gameObject;
    }
}
