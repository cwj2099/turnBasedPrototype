using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleButton : MonoBehaviour
{
    public bool clicked = false;

    void LateUpdate()
    {
        clicked = false;
    }

    public void Click()
    {
        clicked = true;
    }
}
