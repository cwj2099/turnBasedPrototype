using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator thisAnim;
    public SpriteRenderer thisSpriteRenderer;
    public BossController boss;
    public GameObject hitEffect;
    public Text turnText;
    public int attackCounter = 0;

    public int turns = 0;//回合数
    public float betweenTurn = -1;//回合之间的计时变量
    public float timeUnit = 0.25f; //一回合的时间单位
    public float speedUnit = 1;//一回合移动几个移动单位
    public float moveUnit = 5; //移动单位
    public int moveTurns = 0;//移动回合
    public int attackTurns = 0;//攻击回合
    public int invicibleTurns = 0;//无敌回合
    public int waitTurns = 0;//等待回合
    public KeyCode storedInput; //储存输入

    public int position = 0;//当前抽象位置
    public int speed = 0;//移动速度
    public bool facing = true;//面朝方向
    public bool attakcing = false;//是否有攻击判定
    public int attackRange = 0;//攻击范围
    public bool invicible = false;//是否有无敌判定

    // Start is called before the first frame update
    void Start()
    {
        speedUnit *= moveUnit / timeUnit;

    }

    // Update is called once per frame
    void Update()
    {
        //计时环节   
        if (turns > 0)
        {
            //时间恢复正常
            Time.timeScale = 1;
            //回合数归零之前一直计时
            if (betweenTurn > 0)
            {
                betweenTurn -= Time.unscaledDeltaTime;
            }
            //回合结束一次
            else
            {
                //每回合定位一次
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
                turns--;
                //如果需要等待，先等待。如果等待结束，则移动
                if (waitTurns > 0) { waitTurns--; }
                else {
                    if (moveTurns > 0) { moveTurns--; position += speed; }
                    if (attackTurns > 0) { attackTurns--; }
                    if (invicibleTurns > 0) { invicibleTurns--; }
                }

                //伤害检查
                int dir;
                if (thisSpriteRenderer.flipX) { dir = -1; } else { dir = 1; }
                if (attakcing && (Mathf.Abs(boss.position - position) <Mathf.Abs(attackRange*dir))&&(Mathf.Sign(boss.position - position)== Mathf.Sign(attackRange * dir)))
                {
                    print("hitted");
                    Instantiate(hitEffect, boss.transform.position,transform.rotation);
                }
                //重新开始计时
                betweenTurn = timeUnit;
            }
            //如果开始行动
            if (waitTurns == 0)
            {
                //如果仍在移动
                if (moveTurns > 0)
                {
                    transform.Translate(speed * speedUnit * Time.deltaTime, 0, 0);
                    if (speed < 0) { thisSpriteRenderer.flipX = true; }
                    else { thisSpriteRenderer.flipX = false; }
                }
                //是否在攻击/无敌
                attakcing = (attackTurns > 0);
                invicible = (invicibleTurns > 0);
            }


        }
        //更新回合数
        turnText.text = turns.ToString();
        //不是正在进行移动的时候，自动转向
        if (moveTurns <= 0)
        {
            if (boss.position<position) { facing = true; }
            else if (boss.position>position) { facing = false; }
            thisSpriteRenderer.flipX = facing;
        }
        //还有1回合结束时，允许提前输入
        if (turns<=1) {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    //your code here
                    storedInput = vKey;

                }
            }
        }
        //零回合时，根据储存的输入执行新行动
        if (turns==0) {
            //时间暂停
            Time.timeScale = 0;
            //各个变量复位
            speed = 0;attakcing = false;attackRange = 0;invicible = false;
            if (storedInput==KeyCode.A)
            {
                thisAnim.Play("Move");
                startTurns(1);
                speed = -1;
                moveTurns = 1;
            }

            if (storedInput == KeyCode.D)
            {
                thisAnim.Play("Move");
                startTurns(1);
                speed = 1;
                moveTurns = 1;
            }

            if (storedInput == KeyCode.LeftShift)
            {
                thisAnim.Play("Avoid");
                startTurns(3);
                invicibleTurns=2;
                speed = -1;
                moveTurns = 2;
            }

            if (storedInput == KeyCode.L)
            {
                thisAnim.Play("Avoid");
                startTurns(3);
                invicibleTurns = 2;
                speed = 1;
                moveTurns = 2;
            }

            if (storedInput == KeyCode.J)
            {

                if (attackCounter == 0)
                {
                    thisAnim.Play("Attack1");
                    startTurns(2);
                    attackTurns = 1;
                }
                if (attackCounter == 1)
                {
                    thisAnim.Play("Attack2");
                    startTurns(2);
                    attackTurns = 1;
                }
                if (attackCounter == 2)
                {
                    thisAnim.Play("Attack3");
                    startTurns(3);
                    waitTurns = 1;
                    attackTurns = 1;
                }
                attackRange = 2;
                attackCounter++;
                if (attackCounter == 3) { attackCounter = 0; }
            }

            if (storedInput == KeyCode.K)
            {
                startTurns(3);
                waitTurns = 2;
                moveTurns = 1;
                attackTurns = 1;
                thisAnim.Play("PreSpecial");
                if (thisSpriteRenderer.flipX) { speed = -2; }
                else { speed = 2; }
                attackRange = -3;
            }
            storedInput = KeyCode.None;
        }

    }
    void startTurns(int amount)
    {
        turns = amount;
        betweenTurn = timeUnit;
    }
}


