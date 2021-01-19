using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public KeyCode theInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        theInput = KeyCode.None;
    }
    
    public void Left()
    {
        theInput = KeyCode.A;
    }

    public void Right()
    {
        theInput = KeyCode.D;
    }

    public void Attack()
    {
        theInput = KeyCode.J;
    }

    public void Thrust()
    {
        theInput = KeyCode.K;
    }

    public void DashL()
    {
        theInput = KeyCode.LeftShift;
    }

    public void DashR()
    {
        theInput = KeyCode.L;
    }
}
