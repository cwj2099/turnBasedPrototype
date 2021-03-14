using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBlock : BattleEnds
{
    public int index;
    public override void Event()
    {
        base.Event();
        Messenger mes = FindObjectOfType<Messenger>();
        mes.blockCleared[index] = true;
    }
}
