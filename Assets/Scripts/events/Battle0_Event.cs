using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle0_Event : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        Messenger Mes = FindObjectOfType<Messenger>();
        Mes.stoneCleared = true;
    }
}
