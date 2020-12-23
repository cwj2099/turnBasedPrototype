using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public PlayerController player;
    public BossController boss;
    public float betweenTurn = -1;//回合之间的计时变量
    public float halfTurn = -1;//半回合计算
    public float timeUnit = 0.25f; //一回合的时间单位

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //计时环节   
        if (player.turns > 0)
        {
            //时间恢复正常
            Time.timeScale = 1;
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
                    player.attackTurns = 0; player.waitTurns = 0; player.moveTurns = 0;player.attackCounter = 0;
                    player.transform.position = new Vector3(player.position * player.moveUnit, player.transform.position.y, player.transform.position.z);
                    player.turns = 4;
                    player.hurtTurns = 4;
                    player.thisAnim.Play("Hurt");
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
                    ||player.position==boss.position))
                {
                    //print("hitted");
                    Instantiate(player.hitEffect, boss.transform.position, transform.rotation);
                    boss.hitted = true;
                }
                
            }
        }
    }
}
