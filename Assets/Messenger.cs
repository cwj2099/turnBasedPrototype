using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Messenger : MonoBehaviour
{
    public Vector3 CamPosition;
    public Vector3 PlayerPosition;
    public int CamLocation;
    public bool stoneCleared = false;
    public bool monsterCleared = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
