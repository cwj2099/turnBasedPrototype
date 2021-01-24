using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject LButton;
    public GameObject RButton;
    public int location = 0;
    public int startlocation = 0;
    public int endlocation = 3;
    public GameObject[] locators;
    public float changeDuration = 2;
    public float changeCounter = 0;
    public Vector3 camSpeed;
    // Start is called before the first frame update
    void Start()
    {
        endlocation = locators.Length - 1;
        Messenger Mes = FindObjectOfType<Messenger>();
        location = Mes.CamLocation;
        transform.position = Mes.CamPosition;
        player.transform.position = Mes.PlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //位置控制
        if (transform.position != locators[location].transform.position)
        {
            //切换场景
            if (changeCounter > 0)
            {
                transform.position += camSpeed * Time.deltaTime;
                changeCounter -= Time.deltaTime;
            }
            //锁定
            else
            {
                transform.position = locators[location].transform.position;
            }
        }
        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
        
        //切换后的一些表现更改
        if (changeCounter <= 0.01)
        {
            LButton.SetActive(!locators[location].GetComponent<Locator>().noLeft);
            RButton.SetActive(!locators[location].GetComponent<Locator>().noRight);
        }
        else
        {
            LButton.SetActive(false);
            RButton.SetActive(false);
        }
    }

    public void GoLeft()
    {
        if (changeCounter <= 0.01)
        {
            location -= 1;
            location = Mathf.Max(startlocation, location);
            changeCounter = changeDuration;
            camSpeed = (locators[location].transform.position - transform.position) / changeDuration;
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void GoRight()
    {
        if (changeCounter <= 0.01)
        {
            location += 1;
            location = Mathf.Min(endlocation, location);
            changeCounter = changeDuration;
            camSpeed = (locators[location].transform.position - transform.position) / changeDuration;
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
