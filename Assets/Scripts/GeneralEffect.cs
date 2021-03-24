using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffect : MonoBehaviour
{
    private float camMax = 5f;
    private float camMin = 4.3f;

    private float zoonIn = 0.2f;
    private float zoonOut = 0.5f;

    private float stunScale = 0.01f;
    private float stunCounter;
    private float curv1 = 0f;
    private float curv2 = 0f;
    private float curv3 = 0f;
    private bool zooning = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale >0&&Time.timeScale!=1)
        {
            
            if (curv1 > 0)
            {
                Time.timeScale = EasingFunction.EaseInQuart(stunScale, 1, 1 - curv1 / stunCounter);
                curv1 -= Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (zooning)
        {
            Camera.main.orthographicSize = EasingFunction.EaseOutQuad(camMin, camMax, curv3/ zoonIn);
            if (curv3 > 0)
            {
                curv3 -= Time.unscaledDeltaTime;
            }
            else
            {
                curv2 = zoonOut;
                zooning = false;
            }
        }
        else
        {
            Camera.main.orthographicSize = EasingFunction.EaseInQuad(camMin, camMax, 1 - curv2 / zoonOut);
            if (curv2 > 0)
            {
                curv2 -= Time.unscaledDeltaTime;
            }
            else
            {
                Camera.main.orthographicSize = camMax;
            }
        }
    }

    public void time_play()
    {
        if (Time.timeScale == 0) { Time.timeScale = 1; }
        //zooning = false;
        
    }

    public void time_pause()
    {
        Time.timeScale = 0;
        //zooning = true;
        
    }

    public void end()
    {
        Camera.main.orthographicSize = camMax;
        Time.timeScale = 1;
        curv1 = 0;
        curv2 = 0;
        curv3 = 0;
        zooning = false;
    }

    public void hitStun(float time)
    {
        stunCounter = time;
        curv1 = time;
        Time.timeScale = stunScale;
    }

    public void camZoon()
    {
        zooning = true;
        curv3 = zoonIn;
    }
}
