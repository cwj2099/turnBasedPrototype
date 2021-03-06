using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffect : MonoBehaviour
{
    public float camMax = 5f;
    public float camMin = 4.5f;
    public float zoonTime = 0.5f;
    public float recoverTime = 0.5f;

    private float curv1 = 0f;
    private float curv2 = 0f;
    private bool zooning = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (zooning)
        {
            curv2 -= Time.unscaledDeltaTime* (recoverTime / zoonTime);
            curv1 += Time.unscaledDeltaTime;
            curv1 = Mathf.Min(curv1, zoonTime);
            curv2 = Mathf.Max(curv2, 0);
            Camera.main.orthographicSize = EasingFunction.EaseInQuint(camMax, camMin, curv1 / zoonTime);
        }
        else
        {
            curv2 += Time.unscaledDeltaTime;
            curv1 -= Time.unscaledDeltaTime*(zoonTime/recoverTime);
            curv2 = Mathf.Min(curv2, recoverTime);
            curv1 = Mathf.Max(curv1, 0);
            Camera.main.orthographicSize = EasingFunction.EaseOutQuint(camMin, camMax, curv2 / recoverTime);
        }
    }

    public void time_play()
    {
        Time.timeScale = 1;
        zooning = false;
        
    }

    public void time_pause()
    {
        Time.timeScale = 0;
        zooning = true;
        
    }
}
