using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWhenIn : MonoBehaviour
{
    public GameObject toAppear;
    public GameObject locator;
    public CameraController Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        toAppear.SetActive(Cam.locators[Cam.location] == locator && Cam.changeCounter <= 0.01);
    }
}
