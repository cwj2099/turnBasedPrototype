using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator thisAnim;
    public SpriteRenderer thisSpriteRenderer;
    public GameObject boss;
    public int attackCounter = 0;
    public float moveTime = 0;
    public float actionTime = 0;
    public float timeUnit = 0.2f;
    public float moveUnit = 5;
    public KeyCode storedInput;
    public float speed = 0;
    public bool facing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.transform.position.x - this.transform.position.x<-0.1) { facing = true; }
        else if(boss.transform.position.x - this.transform.position.x>0.1) { facing = false; }
        thisSpriteRenderer.flipX = facing;
        if (moveTime <= 0.25f) {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    //your code here
                    storedInput = vKey;

                }
            }
        }

        if (moveTime<=0) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
            Time.timeScale = 0;
            speed = 0;
            if (storedInput==KeyCode.A)
            {
                thisAnim.Play("Move");
                moveTime = timeUnit;
                actionTime = timeUnit;
                speed = moveUnit * 1 / timeUnit;
                storedInput = KeyCode.None;
            }

            if (storedInput == KeyCode.D)
            {
                thisAnim.Play("Move");
                moveTime = timeUnit;
                actionTime = timeUnit;
                speed = -moveUnit * 1 / timeUnit;
                storedInput = KeyCode.None;
            }

            if (storedInput == KeyCode.LeftShift)
            {
                thisAnim.Play("Avoid");
                moveTime = timeUnit * 3;
                actionTime = timeUnit*2;
                speed = moveUnit * 1 / timeUnit;
                storedInput = KeyCode.None;
            }

            if (storedInput == KeyCode.L)
            {
                thisAnim.Play("Avoid");
                moveTime = timeUnit * 3;
                actionTime = timeUnit * 2;
                speed = -moveUnit * 1 / timeUnit ;
                storedInput = KeyCode.None;
            }

            if (storedInput == KeyCode.J)
            {

                if (attackCounter == 0)
                {
                    thisAnim.Play("Attack1");
                    moveTime = timeUnit * 2;
                }
                if (attackCounter == 1)
                {
                    thisAnim.Play("Attack2");
                    moveTime = timeUnit * 2;
                }
                if (attackCounter == 2)
                {
                    thisAnim.Play("Attack3");
                    moveTime = timeUnit * 3;
                }
                storedInput = KeyCode.None;
                attackCounter++;
                if (attackCounter == 3) { attackCounter = 0; }
            }

            if (storedInput == KeyCode.K)
            {
                thisAnim.Play("Special");
                moveTime = timeUnit * 3;
                actionTime = timeUnit * 2;
                if (boss.transform.position.x < this.transform.position.x) { speed = moveUnit * 1 / timeUnit; }
                else { speed = -moveUnit * 1 / timeUnit; }
                
                storedInput = KeyCode.None;
            }
        }
        else
        {
            Time.timeScale = 1;
            moveTime -= Time.deltaTime;
            if (actionTime > 0)
            {
                actionTime -= Time.deltaTime;
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                if (speed > 0) { thisSpriteRenderer.flipX = true; }
                else { thisSpriteRenderer.flipX = false; }
            }
        }
    }
}
