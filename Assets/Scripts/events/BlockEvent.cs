using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    Messenger Mes;
    [SerializeField]
    CameraController Cam;
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
        Cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ob in preBlock)
        {
            ob.SetActive((!Mes.blockCleared[index])&&Cam.locators[Cam.location]==thisLocator.gameObject);
        }
        foreach (GameObject ob in postBlock)
        {
            ob.SetActive((Mes.blockCleared[index] && Cam.locators[Cam.location] == thisLocator.gameObject));
        }

        thisLocator.noRight = (!Mes.blockCleared[index]&&blockRight);
        thisLocator.noLeft = (!Mes.blockCleared[index] && blockLeft);
    }
}
