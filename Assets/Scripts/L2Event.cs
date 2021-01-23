using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2Event : MonoBehaviour
{
    Messenger Mes;
    public GameObject stone;
    public GameObject button;
    public GameObject text1;
    public GameObject textController1;
    public GameObject textController2;
    public Locator thisLocator;
    // Start is called before the first frame update
    void Start()
    {
        Mes= FindObjectOfType<Messenger>();
    }

    // Update is called once per frame
    void Update()
    {
        stone.SetActive(!Mes.stoneCleared);
        if (Mes.stoneCleared) { text1.SetActive(false); button.SetActive(false); }
        textController1.SetActive(!Mes.stoneCleared);
        textController2.SetActive(Mes.stoneCleared);
        thisLocator.noRight = (!Mes.stoneCleared);
    }
}
