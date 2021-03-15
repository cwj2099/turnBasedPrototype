using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float Hp;
    public float MaxHp;
    public float damage;

    public PlayerController player;
    public gameManager GM;
    public Animator thisAnim;
    public Text turnText;
    public Image healthBar;
    public SpriteRenderer thisSpriteRenderer;
    public bool hitted = false;
    public GameObject hitEffect;
    public SpriteRenderer pre1;
    public SpriteRenderer pre2;

    public int turns = 0;//回合数 Turn Counter 
    public float timeUnit;//一回合的时间单位 how long is a turn
    public float speedUnit = 1;//一回合移动几个移动单位 how many unit do enemy move in a turn
    public float moveUnit = 2; //移动单位 how long in unity, is a unit
    public float moveTurns = 0;//移动回合 the turn numbers of moving
    public float pushedTurns = 0;//被击退回合 the turn numbers of pushed 
    public float attackTurns = 0;//攻击回合 the turn numbers of causing attack
    public float invicibleTurns = 0;//无敌回合 the turn numbers of invicible
    public float waitTurns = 0;//等待回合 the turn numbers of waiting, for things listed above

    public int position = 2;//当前抽象位置 the current posistion, in abstract
    public int speed = 0;//移动速度 the current abstract speed
    public int pushedSpeed = 0; //被移动速度 the current abstract pushed speed
    public bool facing = true;//面朝方向 the facing
    public bool attakcing = false;//是否有攻击判定 if are causing attacks
    public int attackRange = 0;//攻击范围 the range of attack
    public int pushBack = 1; 
    public bool invicible = false;//是否有无敌判定
    public float priority = 0;
    bool lastAct=false;

    public Color preColor;
    public Color attackColor;
    public Color afterColor;
    // Start is called before the first frame update
    void Start()
    {
        timeUnit = GM.timeUnit;
        speedUnit *= moveUnit / timeUnit;
        thisAnim.SetFloat("unit", 1 / timeUnit);
        Hp = MaxHp;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        pre1.enabled = false;
        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            pre1.flipX = thisSpriteRenderer.flipX;
            if (waitTurns > 0) { 
                pre1.enabled = true;
                Color c = pre1.color;
                c.a = 1.5f - ((waitTurns-2) / 2);
                pre1.color = c;
            }
        }

        pre2.enabled = false;
        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")|| thisAnim.GetCurrentAnimatorStateInfo(0).IsName("preAttack1"))
        {
            pre2.flipX = thisSpriteRenderer.flipX;
            if (waitTurns > 0)
            {
                pre2.enabled = true;
                Color c1 = pre2.color;
                c1.a = 1.5f - ((waitTurns - 2) / 2);
                pre2.color = c1;
            }
        }
        //更新回合数
        if (waitTurns > 0)
        {
            turnText.text = waitTurns.ToString();
            turnText.color = attackColor;
        }
        /*else if(attackTurns > 0)
        {
            turnText.text = attackTurns.ToString();
            turnText.color = attackColor;
        }*/
        else
        {
            turnText.text = turns.ToString();
            turnText.color = afterColor;
        }
        

        //不在攻击或者移动的时候自动转向
        //if (moveTurns <= 0 && attackTurns <= 0 && waitTurns <=0)
        //只能在快结束了转向
        if (turns<=2 && !GM.gameOver)
        {
            /* if (player.position < position) { facing = false; }
             else if (player.position > position) { facing = true; }
             thisSpriteRenderer.flipX = facing;*/
            thisSpriteRenderer.flipX = player.thisSpriteRenderer.flipX;
            facing = thisSpriteRenderer.flipX;
        }

        if (turns == 0)
        {
            /*if (Mathf.Abs(player.position-position)%2==0)
            {
                thisAnim.Play("Attack1");
                startTurns(7);
                waitTurns = 2;
                attackTurns = 1;
                attackRange = 2;
            }
            else
            {
                thisAnim.Play("Attack2");
                startTurns(20);
                waitTurns = 4;
                moveTurns = 1;
                if (thisSpriteRenderer.flipX) { speed = 1; } else { speed = -1; }
                attackTurns = 2;
                attackRange = 2;
            }*/
            if(lastAct)
            {
                lastAct = false;
                thisAnim.Play("preAttack1");
                startTurns(14);
                waitTurns = 4;
                attackTurns = 1;
                attackRange = 3;
                damage = 1;
            }
            else
            {
                lastAct = true;
                thisAnim.Play("Attack2");
                startTurns(24);
                waitTurns = 4;
                moveTurns = 1;
                if (thisSpriteRenderer.flipX) { speed = 1; } else { speed = -1; }
                attackTurns = 1;
                attackRange = 2;
                damage = 1;
            }
        }

        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            if (turns == 16)
            {
                thisSpriteRenderer.flipX = player.thisSpriteRenderer.flipX;
                attakcing = false;
                waitTurns = 4;
                attackTurns = 1;
                moveTurns = 1;
                if (thisSpriteRenderer.flipX) { speed = 1; } else { speed = -1; }
                attackRange = 2;
            }
            if (turns > 16 && waitTurns == 0)
            {
                turnText.text = (turns-16).ToString();
            }
        }

        
    }
    void startTurns(int amount)
    //初始化回合，并非真的回合开始
    {
        turns = amount;
    }

    public virtual void halfTurn()
    //回合过半时执行一次
    {

        //如果不再等待
        if (waitTurns == 0)
        {
            //根据速度更新抽象位置
            if (moveTurns > 0) { position += speed; }
            position = Mathf.Max(-3, position); position = Mathf.Min(3, position);



            //更新攻击与无敌状态
            attakcing = (attackTurns > 0);
            invicible = (invicibleTurns > 0);
        }
       
        
    }

    public virtual void wholeTurn()
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
                if (speed > 0) { thisSpriteRenderer.flipX = true; }
                else { thisSpriteRenderer.flipX = false; }
            }

        }
        //如果被推也改
        if (pushedTurns > 0)
        {
            transform.Translate(pushedSpeed * speedUnit * Time.deltaTime, 0, 0);
            transform.position = new Vector3(Mathf.Max(-7.5f, transform.position.x), transform.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Min(7.5f, transform.position.x), transform.position.y, transform.position.z);
        }

    }

    public virtual void endTurn()
    //每次回合结束执行一次
    {
        //根据被推改变位置
        if (pushedTurns > 0) { position += pushedSpeed; }
        position = Mathf.Max(-3, position); position = Mathf.Min(3, position);

        //实际位置重定位
        transform.position = new Vector3(moveUnit * position, transform.position.y, transform.position.z);

        turns--; turns = Mathf.Max(0, turns);
        //回合数更新
        if (waitTurns > 0) { waitTurns --; }
        else
        {
            if (moveTurns > 0) { moveTurns--; }
            if (attackTurns > 0) { attackTurns--; }
            if (invicibleTurns > 0) { invicibleTurns--; }
        }
        if (pushedTurns > 0) { pushedTurns--; }
    }
}
