using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public dialogue dia;
    
    public void triggerDialogue()
    {
        FindObjectOfType<dialogueManager>().startDialogue(dia);
    }
}
