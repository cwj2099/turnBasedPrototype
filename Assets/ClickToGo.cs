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
        Mes.CamLocation = Cam.location;
        Mes.CamPosition = Cam.gameObject.transform.position;
    }

}
