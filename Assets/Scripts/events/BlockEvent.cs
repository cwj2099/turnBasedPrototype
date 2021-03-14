using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    Messenger Mes;
    public int index;
    public GameObject[] preBlock;
    public GameObject[] postBlock;
    public Locator thisLocator;
    public bool blockLeft;
    public bool blockRight;
    // Start is called before the first frame update
    void Start()
    {
        Mes= FindObjectOfType<Messenger>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ob in preBlock)
        {
            ob.SetActive(!Mes.blockCleared[index]);
        }
        foreach (GameObject ob in postBlock)
        {
            ob.SetActive(Mes.blockCleared[index]);
        }

        thisLocator.noRight = (!Mes.blockCleared[index]&&blockRight);
        thisLocator.noLeft = (!Mes.blockCleared[index] && blockLeft);
    }
}
