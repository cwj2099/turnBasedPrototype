﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Hp;
    public float Mp;
    public float damage;

    public Animator thisAnim;
    public SpriteRenderer thisSpriteRenderer;
    public gameManager GM;
    public GeneralEffect effector;
    public BossController boss;
    public ButtonInput bInput;
    public GameObject hitEffect;
    public GameObject hitEffect2;
    public GameObject chargeEffect;
    public Text turnText;
    public Image healthBar;
    public Image MpBar;
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
    public static string input;

    public int position = 0;//当前抽象位置
    public int speed = 0;//移动速度
    public bool facing = true;//面朝方向
    public bool attakcing = false;//是否有攻击判定
    public int attackRange = 0;//攻击范围
    public int pushBack = 0;//击退距离
    public bool invicible = false;//是否有无敌判定
    public float priority = 0.5f;//确认是么时候发生

    public Color nColor;
    public Color iColor;

    public Image attackButton;
    public Sprite a2;
    public Sprite a3;

    public AudioSource BreakSound;
    public AudioSource BlockSound;
    public AudioSource SlashSound;
    public AudioSource HitSound1;
    public AudioSource HitSound2;
    public AudioSource HitSound3;
    public AudioSource HurtSound;
    // Start is called before the first frame update
    void Start()
    {
        timeUnit = GM.timeUnit;
        speedUnit *= moveUnit / timeUnit;
        thisAnim.SetFloat("unit", 1 / timeUnit);

        HurtSound.timeSamples = 20000;
        HitSound3.timeSamples = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        MpBar.fillAmount = Mp / 3;
      //  if (GM.gameOver) { thisAnim.Play("idle"); }
        if (invicible) { thisSpriteRenderer.color = iColor; }
        else { thisSpriteRenderer.color = nColor; }
        /*if (attackCounter == 2) { attackButton.sprite = a3; attackButton.rectTransform.sizeDelta = new Vector2(270, 270); }
        else { attackButton.sprite = a2; attackButton.rectTransform.sizeDelta = new Vector2(250, 250); }*/
        //更新回合数
        turnText.text = turns.ToString();
        //不是正在进行移动或者攻击的时候，自动转向
        if (turns<=0&&!GM.gameOver)
        {
            if (boss.position<position) { facing = true; }
            else if (boss.position>position) { facing = false; }
            thisSpriteRenderer.flipX = facing;
        }
        //还有1回合结束时，允许提前输入
        if (turns<=1) {

            if (bInput.theInput != KeyCode.None)
            {
                print(bInput.theInput);
                //storedInput = bInput.theInput;
            }
            else
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(vKey))
                    {
                        //your code here
                        storedInput = vKey;
                        /*if (storedInput == KeyCode.Mouse0)
                        {
                            print("mouse");
                            storedInput = KeyCode.None;
                        }*/
                        switch (storedInput)
                        {
                            case KeyCode.A:
                                input = "Left";
                                break;
                            case KeyCode.D:
                                input = "Right";
                                break;
                            case KeyCode.J:
                                input = "Attack";
                                break;
                            case KeyCode.K:
                                input = "Block";
                                break;

                        }

                    }
                }
            }
            
        }
        //板边防御结束被破防
        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Charge") && turns==1&&(position==-3||position==3)) {
            
        }

        //零回合时，根据储存的输入执行新行动
        if (turns<=0) {
            //时间暂停
            effector.time_pause();
            //各个变量复位
            speed = 0;attakcing = false;attackRange = 0;invicible = false;
            if (input== "Left")
            {
                if(Mathf.Abs(position - boss.position) <= 2&&position>=boss.position&&!boss.invicible)
                {
                    SlashSound.Play();
                    startTurns(2);
                    //waitTurns = 1;
                    moveTurns = 1;
                    attackTurns = 1;
                    thisAnim.Play("Special");
                    speed = -2;
                    attackRange = -3;
                    damage = 1;
                    pushBack = 0;
                    if (Mp > 0) { thisAnim.Play("Speical_enhanced"); damage = 3; }
                }
                else
                {
                    thisAnim.Play("Move");
                    startTurns(2);
                    speed = -2;
                    moveTurns = 1;
                }

            }

            if (input == "Right")
            {
                if (Mathf.Abs(position - boss.position) <= 2 && position <= boss.position && !boss.invicible)
                {
                    SlashSound.Play();
                    startTurns(2);
                    //waitTurns = 1;
                    moveTurns = 1;
                    attackTurns = 1;
                    thisAnim.Play("Special");
                    speed = 2;
                    attackRange = -3;
                    damage = 1;
                    pushBack = 0;
                    if (Mp > 0) { thisAnim.Play("Speical_enhanced"); damage = 3; }
                }
                else {
                    thisAnim.Play("Move");
                    startTurns(2);
                    speed = 2;
                    moveTurns = 1;
                }


            }

            /*if (storedInput == KeyCode.LeftShift)
            {
                thisAnim.Play("Avoid");
                startTurns(3);
                invicibleTurns=2;
                speed = -1;
                moveTurns = 2;
            }*/


            /*if (storedInput == KeyCode.L)
            {
                thisAnim.Play("Avoid");
                startTurns(3);
                invicibleTurns = 2;
                if (thisSpriteRenderer.flipX) { speed = 2; }
                else { speed = -2; }
                moveTurns = 1   ;
            }*/

            if (input == "Attack")
            {

                if (attackCounter == 0)
                {
                    SlashSound.Play();
                    thisAnim.Play("Attack1");
                    startTurns(2);
                    /*if (Mathf.Abs(boss.position - position) > 1)
                    {
                        thisAnim.Play("Special");
                        moveTurns = 1;
                        if (thisSpriteRenderer.flipX) { speed = boss.position - position+1; }
                        else { speed = boss.position - position-1; }
                    }*/
                    attackTurns = 1;
                    attackRange = 3;
                    damage = 2;
                    pushBack = 0;
                    priority = 0.1f;
                    if (Mp > 0) { thisAnim.Play("Attack1_enhanced"); damage = 3; attackRange = 4; }
                }
                if (attackCounter == 1)
                {
                    SlashSound.Play();
                    thisAnim.Play("Attack2");
                    startTurns(2);
                    attackTurns = 1;
                    attackRange = 3;
                    damage = 2;
                    pushBack = 0;
                    priority = 0.1f;
                    if (Mp > 0) { thisAnim.Play("Attack2_enhanced");damage = 3; attackRange = 4; }
                }
                if (attackCounter == 2)
                {
                    SlashSound.Play();
                    thisAnim.Play("Attack3");
                    startTurns(2);
                    //waitTurns = 1;
                    attackTurns = 1;
                    attackRange = 3;
                    damage = 3;
                    pushBack = 1;
                    priority = 0.01f;
                    if (/*Mp > 0*/true) { thisAnim.Play("Attack3_enhanced"); damage = 6; attackRange = 4; }
                }

                attackCounter++;
                if (attackCounter == 3) { attackCounter = 0; }
                
            }
            else if(input != null){ attackCounter = 0; }

            if (input == "Block")
            {
                /*damage = 1;
                startTurns(2);
                //waitTurns = 1;
                moveTurns = 1;
                attackTurns = 1;
                thisAnim.Play("Special");
                if (thisSpriteRenderer.flipX) { speed = -2; }
                else { speed = 2; }
                attackRange = -3;*/
                startTurns(2);
                thisAnim.Play("Charge");
                Mp++;
                Instantiate(chargeEffect, transform.position, transform.rotation);
            }
            Mp = Mathf.Min(Mp, 3);
            input = null;
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
            position = Mathf.Max(-3, position); position = Mathf.Min(3, position);
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
                transform.position = new Vector3(Mathf.Max(-7.5f, transform.position.x), transform.position.y, transform.position.z);
                transform.position = new Vector3(Mathf.Min(7.5f, transform.position.x), transform.position.y, transform.position.z);
                if (!thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
                {
                    if (speed < 0) { thisSpriteRenderer.flipX = true; }
                    else { thisSpriteRenderer.flipX = false; }
                }
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

    public void GetInput(string key)
    {
        input = key;
        print(key);

    }
}


