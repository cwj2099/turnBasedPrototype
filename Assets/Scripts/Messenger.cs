using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Messenger : MonoBehaviour
{
    public bool theFirst = false;
    public Vector3 CamPosition;
    public Vector3 PlayerPosition;
    public int CamLocation;
    public bool[] blockCleared;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(FindObjectOfType<Messenger>()&& FindObjectOfType<Messenger>().theFirst)
        {
            Destroy(this.gameObject);
        }
        theFirst = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
