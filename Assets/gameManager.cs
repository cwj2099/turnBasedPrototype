using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public PlayerController player;
    public BossController boss;
    public float betweenTurn = -1;//回合之间的计时变量
    public float halfTurn = -1;//半回合计算
    public float timeUnit = 0.25f; //一回合的时间单位
    public bool gameOver = false;
    public bool playerWin;
    public float counter0 = 0;
    public Image black;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //计时环节   
        if (player.turns > 0&&!gameOver)
        {
            //时间恢复正常
            Time.timeScale = 1;
            /*if (player.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                Time.timeScale = 0.5f;
            }*/

            player.wholeTurn();
            boss.wholeTurn();

            //回合数归零之前一直计时
            if (betweenTurn > 0)
            {
                betweenTurn -= Time.deltaTime;
            }
            //回合结束
            else
            {
                player.endTurn();
                boss.endTurn();
                boss.halfTurn();
                //敌人打玩家
                int dir;
                if (boss.thisSpriteRenderer.flipX) { dir = 1; } else { dir = -1; }

                if (boss.attakcing && !player.invicible && player.hurtTurns <= 0 && (((Mathf.Abs(player.position - boss.position) < Mathf.Abs(boss.attackRange * dir))
                    && (Mathf.Sign(player.position - boss.position) == Mathf.Sign(boss.attackRange * dir)))
                    || player.position == boss.position))
                {
                    //print("hitted");
                    Instantiate(boss.hitEffect, player.transform.position, transform.rotation);
                    if (player.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Charge")) {
                        if (!(player.position==3||player.position==-3)) {
                            player.turns = 1;
                            player.thisAnim.Play("Charge");
                            player.moveTurns = 1;
                            player.Mp++;
                            if (!boss.facing) { player.speed = -1*boss.pushBack; }
                            else { player.speed = boss.pushBack; }
                        }
                        else
                        {
                            player.thisAnim.Play("Hurt");
                            player.turns = 3;
                        }
                    }
                    else
                    {
                        player.attackTurns = 0; player.waitTurns = 0; player.moveTurns = 0; player.attackCounter = 0;
                        player.transform.position = new Vector3(player.position * player.moveUnit, player.transform.position.y, player.transform.position.z);
                        player.turns = 2;
                        player.hurtTurns = 2;
                        player.thisAnim.Play("Hurt");
                        player.Hp = 0;
                    }
                    //player.healthBar.fillAmount = player.Hp / 3;
                    if (!gameOver && player.Hp <= 0)
                    {
                        gameOver = true;
                        Destroy(player.gameObject);
                        playerWin = false;
                        counter0 = 2;
                    }
                }

                betweenTurn = timeUnit;

            }
            //半回合计时
            if (halfTurn > 0)
            {
                halfTurn -= Time.deltaTime;
            }
            //半回合结束
            else
            {
                //时间重置
                halfTurn = timeUnit;
                //print("half");
                //玩家更新
                player.halfTurn();
                

                //伤害检查
                //获得玩家攻击方向
                int dir;
                if (player.thisSpriteRenderer.flipX) { dir = -1; } else { dir = 1; }
                //如果玩家和敌人重合，或者玩家攻击距离弥补了两者之间的距离且两个数值方向一致时，造成伤害
                if (player.attakcing && (((Mathf.Abs(boss.position - player.position) < Mathf.Abs(player.attackRange * dir)) 
                    && (Mathf.Sign(boss.position - player.position) == Mathf.Sign(player.attackRange * dir)))
                    ||player.position==boss.position)&&(!boss.invicible))
                {
                    //print("hitted");
                    Instantiate(player.hitEffect, boss.transform.position, transform.rotation);
                    if (player.Mp > 0) { player.Mp--; /*player.damage *= 2;*/ Instantiate(player.hitEffect2, boss.transform.position, transform.rotation); }
                    boss.hitted = true;
                    boss.Hp -= player.damage;
                    boss.healthBar.fillAmount = boss.Hp / boss.MaxHp;
                    if (!gameOver&&boss.Hp <= 0)
                    {
                        gameOver = true;
                        Destroy(boss.gameObject);
                        playerWin = true;
                        counter0 = 2;
                    }
                }
                
            }
        }
        if (gameOver && counter0 > 0)
        {
            Time.timeScale = 1;
            counter0 -= Time.deltaTime;

            Color c = black.color;
            c.a = (2 - counter0) / 2;
            black.color = c;
        }
        else if (gameOver && counter0 <= 0)
        {
            if (playerWin)
            {
                SceneManager.LoadScene("YouWin");
            }
            else
            {
                SceneManager.LoadScene("YouLose");
            }
        }
    }
}
