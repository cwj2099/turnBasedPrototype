using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public PlayerController player;
    public gameManager GM;
    public Animator thisAnim;
    public Text turnText;
    public SpriteRenderer thisSpriteRenderer;
    public bool hitted = false;
    public GameObject hitEffect;

    public int turns = 0;//回合数
    public float timeUnit = 0.25f; //一回合的时间单位
    public float speedUnit = 1;//一回合移动几个移动单位
    public float moveUnit = 5; //移动单位
    public float moveTurns = 0;//移动回合
    public float attackTurns = 0;//攻击回合
    public float invicibleTurns = 0;//无敌回合
    public float waitTurns = 0;//等待回合

    public int position = 2;//当前抽象位置
    public int speed = 0;//移动速度
    public bool facing = true;//面朝方向
    public bool attakcing = false;//是否有攻击判定
    public int attackRange = 0;//攻击范围
    public bool invicible = false;//是否有无敌判定

    public Color preColor;
    public Color attackColor;
    public Color afterColor;
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
        if (turns<=2)
        {
            if (player.position < position) { facing = false; }
            else if (player.position > position) { facing = true; }
            thisSpriteRenderer.flipX = facing;
        }

        if (turns == 0)
        {
            if (Mathf.Abs(player.position-position)<2)
            {
                thisAnim.Play("Attack1");
                startTurns(12);
                waitTurns = 4;
                attackTurns = 1;
                attackRange = 2;
            }
            else
            {

            }
        }
    }
    void startTurns(int amount)
    //初始化回合，并非真的回合开始
    {
        turns = amount;
    }

    public void halfTurn()
    //回合过半时执行一次
    {
        //如果不再等待
        if (waitTurns == 0)
        {
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
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        turns--;turns = Mathf.Max(0, turns);
        //回合数更新
        if (waitTurns > 0) { waitTurns --; }
        else
        {
            if (moveTurns > 0) { moveTurns--; }
            if (attackTurns > 0) { attackTurns--; }
            if (invicibleTurns > 0) { invicibleTurns--; }
        }
    }
}
