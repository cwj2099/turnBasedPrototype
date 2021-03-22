//the whole fsm is based on what we learned in class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//making them monobehaviour, thus I could switch them easily by assigning different script to different game objects
public abstract class Boss2_state:MonoBehaviour
{
    public abstract void EnterState(Boss2_controller boss);
    public abstract void Process(Boss2_controller boss);
    public abstract void LeaveState(Boss2_controller boss);
}
