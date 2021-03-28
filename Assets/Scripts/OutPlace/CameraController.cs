using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public bool inEvent = false;
    public GameObject player;
    public Animator pAnimator;
    public GameObject LButton;
    public GameObject RButton;
    public int location = 0;
    public int startlocation = 0;
    public int endlocation = 3;
    public GameObject[] locators;
    public float changeDuration = 2;
    public float changeCounter = 0;
    public Vector3 camSpeed;
    public Animator diaAni;
    Messenger Mes;
    [SerializeField]
    UnityEvent[] winTalk;
    [SerializeField]
    UnityEvent[] loseTalk;
    // Start is called before the first frame update
    void Start()
    {
        endlocation = locators.Length - 1;
        Mes = FindObjectOfType<Messenger>();
        location = Mes.CamLocation;
        transform.position = Mes.CamPosition;
        player.transform.position = Mes.PlayerPosition;
        if (Mes.battled) {
            talkAfterBattle(location);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //位置控制 Position Controll
        if (transform.position != locators[location].transform.position)
        {
            //切换场景 Change Section
            if (changeCounter > 0)
            {
                transform.position += camSpeed * Time.deltaTime;
                changeCounter -= Time.deltaTime;
            }
            //锁定 Lock when not moving
            else
            {
                transform.position = locators[location].transform.position;
            }
        }
        //if not in events
        if (!inEvent)
        {
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
            if (changeCounter <= 0.01)
            {
                pAnimator.Play("idle");
            }
        }
        //切换后的一些表现更改 make buttons activate when changed
        if (changeCounter <= 0.01&&!diaAni.GetBool("isOpen"))
        {
            //buttons will be on if not border!
            LButton.SetActive(!locators[location].GetComponent<Locator>().noLeft);
            RButton.SetActive(!locators[location].GetComponent<Locator>().noRight);

            //player can also move with keyboard 
            if (Input.GetKeyDown(KeyCode.A)&& !locators[location].GetComponent<Locator>().noLeft) { GoLeft(); }
            else if (Input.GetKeyDown(KeyCode.D)&& !locators[location].GetComponent<Locator>().noRight) { GoRight(); }
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
            pAnimator.Play("player_move");
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
            pAnimator.Play("player_move");
            location += 1;
            location = Mathf.Min(endlocation, location);
            changeCounter = changeDuration;
            camSpeed = (locators[location].transform.position - transform.position) / changeDuration;
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);
        Messenger Mes = FindObjectOfType<Messenger>();
        GameObject Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Mes.CamLocation = location;
        Mes.CamPosition = gameObject.transform.position;
        Mes.PlayerPosition = Player.transform.position;
    }

    public void talkAfterBattle(int id)
    {
        Mes.battled = false;
        if (Mes.won)
        {
            winTalk[id].Invoke();
        }
        else
        {
            loseTalk[id].Invoke();
        }
    }

    
}
