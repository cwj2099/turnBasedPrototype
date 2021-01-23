using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Event : MonoBehaviour
{
    public Locator trigger;
    public bool started = false;
    public CameraController Cam;
    public GameObject player;
    public float speed;
    public float duration=2;
    public float counter=0;
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam.locators[Cam.location] == trigger.gameObject&&Cam.changeCounter<=0.1)
        {
            //print("alala");
            if (!started)
            {
                started = true;
                counter = duration;
            }
            else
            {
                if (counter > 0)
                {
                    counter -= Time.deltaTime;
                    player.transform.Translate(0, speed * Time.deltaTime, 0);
                }
                else
                {
                    Cam.location +=1;
                    Cam.changeCounter = Cam.changeDuration;
                    Cam.camSpeed = (Cam.locators[Cam.location].transform.position - Cam.transform.position) / Cam.changeDuration;
                }
            }
        }
    }
}
