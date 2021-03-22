using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToGo : button
{
    public string toGo;


    public override void GetClicked()
    {
        base.GetClicked();
        SceneManager.LoadScene(toGo);
        Messenger Mes= FindObjectOfType<Messenger>();
        GameObject Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Mes.CamLocation = Cam.location;
        Mes.CamPosition = Cam.gameObject.transform.position;
        Mes.PlayerPosition = Player.transform.position;
    }

}
