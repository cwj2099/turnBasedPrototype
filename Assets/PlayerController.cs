﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator thisAnim;
    public SpriteRenderer thisSpriteRenderer;
    public gameManager GM;
    public BossController boss;
    public GameObject hitEffect;
    public Text turnText;
    public int attackCounter = 0;

    public int turns = 0;//回合数
    public float timeUnit = 0.25f; //一回合的时间单位
    public float speedUnit = 1;//一回合移动几个移动单位
    public float moveUnit = 5; //移动单位
    public float moveTurns = 0;//移动回合
    public float attackTurns = 0;//攻击回合
    public float invicibleTurns = 0;//无敌回合
    public float hurtTurns = 0;//挨打回合
    public float waitTurns = 0;//等待回合
    public KeyCode storedInput; //储存输入

    public int position = 0;//当前抽象位置
    public int speed = 0;//移动速度
    public bool facing = true;//面朝方向
    public bool attakcing = false;//是否有攻击判定
    public int attackRange = 0;//攻击范围
    public bool invicible = false;//是否有无敌判定

    public Color nColor;
    public Color iColor;

    // Start is called before the first frame update
    void Start()
    {
        timeUnit = GM.timeUnit;
        speedUnit *= moveUnit / timeUnit;
        thisAnim.SetFloat("unit", 1 / timeUnit);
    }

    // Update is called once per frame
    void Update()
    {
        if (invicible) { thisSpriteRenderer.color = iColor; }
        else { thisSpriteRenderer.color = nColor; }
        //更新回合数
        turnText.text = turns.ToString();
        //不是正在进行移动或者攻击的时候，自动转向
        if (moveTurns <= 0&&attackTurns<=0)
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
            else if(storedInput!=KeyCode.None){ attackCounter = 0; }

            if (storedInput == KeyCode.K)
            {
                startTurns(3);
                waitTurns = 1;
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
    //初始化回合，并非真的回合开始
    {
        turns = amount;
        GM.betweenTurn = timeUnit;
        GM.halfTurn = timeUnit / 2;
    }

    public void halfTurn()
     //回合过半时执行一次
    {
        //如果不再等待
        if (waitTurns ==0) {
            //根据速度更新抽象位置
            if (moveTurns > 0) { position += speed; }
            //更新攻击与无敌状态
            attakcing = (attackTurns > 0);
            invicible = (invicibleTurns > 0);
        }
    }

    public void wholeTurn()
     //只要有回合，就会进行下去
    {
        //如果不在等待
        if (waitTurns == 0)
        {
            //如果在移动，根据速度改变实际位置
            if (moveTurns > 0)
            {
                transform.Translate(speed * speedUnit * Time.deltaTime, 0, 0);
                if (speed < 0) { thisSpriteRenderer.flipX = true; }
                else { thisSpriteRenderer.flipX = false; }
            }

        }
        
    }

    public void endTurn()
    //每次回合结束执行一次
    {
        //实际位置重定位
        transform.position = new Vector3(position * moveUnit, transform.position.y, transform.position.z);
        turns--;
        //回合数更新
        if (hurtTurns > 0) { hurtTurns--; }
        if (waitTurns > 0) { waitTurns --; }
        else
        {
            if (moveTurns > 0) { moveTurns--; }
            if (attackTurns > 0) { attackTurns--; }
            if (invicibleTurns > 0) { invicibleTurns--; }
        }
    }
}


