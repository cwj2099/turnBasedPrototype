using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    public KeyCode theInput
    {
        set { }
        get { return (_theInput); }
    }
    [SerializeField]
    KeyCode _theInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_theInput != KeyCode.None)
        {
            print(_theInput);
            //storedInput = bInput.theInput;
        }
    }
    void LateUpdate()
    {
        
        _theInput = KeyCode.None;
        
    }
    
    public void Left()
    {
        //print(theInput);
        _theInput = KeyCode.A;
    }

    public void Right()
    {
        //print(theInput);
        _theInput = KeyCode.D;
        
    }

    public void Attack()
    {
        //print(theInput);
        _theInput = KeyCode.J;
    }

    public void Thrust()
    {
        //print(theInput);
        _theInput = KeyCode.K;
    }

    public void DashL()
    {
        //print(theInput);
        _theInput = KeyCode.LeftShift;
    }

    public void DashR()
    {
        //print(theInput);
        _theInput = KeyCode.L;
    }
}
