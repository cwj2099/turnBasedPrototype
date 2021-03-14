using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is only used for special case
public class PerBlock : MonoBehaviour
{
    [SerializeField]
    public Locator locator;
    bool blockLeft;
    bool blockRight;

    void Awake()
    {
        blockLeft = locator.noLeft;
        blockRight = locator.noRight;
    }


    void Update()
    {
        if (blockLeft) { locator.noLeft = true; }
        if (blockRight) { locator.noRight = true; }
    }
}
