using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    public GeneralEffect effector;
    public PlayerController player;
    public BossController boss;
    public float betweenTurn = -1;//回合之间的计时变量
    public float halfTurn = -1;//半回合计算
    public float timeUnit = 0.25f; //一回合的时间单位
    public bool gameOver = false;
    public bool playerWin;
    public float counter0 = 0;
    public Image black;

    private bool playerChecked;
    private bool bossChecked;
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
            effector.time_play();
            /*if (player.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                Time.timeScale = 0.5f;
            }*/

            player.wholeTurn();
            boss.wholeTurn();

            //回合数归零之前一直计时
            if (betweenTurn<=player.priority&&!playerChecked) {  playerCheck(); }
            //if (betweenTurn<=boss.priority&&!bossChecked) {  bossCheck(); }
            if (betweenTurn > 0)
            {
                betweenTurn -= Time.deltaTime;
            }
            //回合结束
            else
            {
                player.endTurn();
                boss.endTurn();
                playerChecked = false;
                bossChecked = false;
                bossCheck();
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
               // playerCheck();
                               
            }
        }
        if (!gameOver && player.Hp <= 0 && player.hurtTurns == 0)
        {
            gameOver = true;
            Destroy(player.gameObject);
            playerWin = false;
            counter0 = 2;
        }
        if (gameOver && counter0 > 0)
        {
            effector.time_play();
            counter0 -= Time.deltaTime;

            Color c = black.color;
            c.a = (2 - counter0) / 2;
            black.color = c;
        }
        else if (gameOver && counter0 <= 0)
        {
            effector.end();
            if (playerWin)
            {
                if (gameObject.GetComponent<BattleEnds>())
                {
                    gameObject.GetComponent<BattleEnds>().Event();
                }
                SceneManager.LoadScene("out1");
            }
            else
            {
                SceneManager.LoadScene("out1");
            }
        }
    }

    void playerCheck()
    {
        playerChecked = true;
        //玩家更新
        player.halfTurn();

        //伤害检查
        //获得玩家攻击方向
        int dir;
        if (player.thisSpriteRenderer.flipX) { dir = -1; } else { dir = 1; }
        //如果玩家和敌人重合，或者玩家攻击距离弥补了两者之间的距离且两个数值方向一致时，造成伤害
        if (player.attakcing && (((Mathf.Abs(boss.position - player.position) < Mathf.Abs(player.attackRange * dir))
            && (Mathf.Sign(boss.position - player.position) == Mathf.Sign(player.attackRange * dir)))
            || player.position == boss.position) && (!boss.invicible))
        {
            //print("hitted");
            
            
            if (player.damage > 4)
            {
                Instantiate(player.hitEffect2, boss.transform.position, transform.rotation);
            }
            else
            {
                GameObject ef = Instantiate(player.hitEffect, boss.transform.position, transform.rotation);
                ef.transform.localScale = new Vector3(ef.transform.localScale.x * dir, ef.transform.localScale.y, ef.transform.localScale.z);
                if (player.facing) { ef.GetComponent<hitEffect>().flipX(); }
            }
            if (player.Mp > 0) { player.Mp--; /*player.damage *= 2;*/ }
            boss.hitted = true;
            boss.Hp -= player.damage;
            boss.health.healthBar_Update(boss.Hp / boss.MaxHp);

            //effector.hitStun(player.damage/30+0.05f/(player.damage*player.damage));
            if (player.damage <= 2) { effector.hitStun(0.02f); }
            else if (player.damage <= 4) { effector.hitStun(0.05f); }
            else { effector.hitStun(0.1f); }
            //击退boss
            /*if (player.facing) { boss.position+= -1 * player.pushBack;boss.transform.Translate(-boss.moveUnit * player.pushBack, 0, 0); }
            else { boss.position+= player.pushBack; boss.transform.Translate(boss.moveUnit * player.pushBack, 0, 0); }*/
            if (player.pushBack > 0)
            {
                //effector.camZoon();
                boss.pushedTurns = 1;
                //boss.turns+=2;
                if (player.facing) { boss.pushedSpeed = -1 * player.pushBack; }
                else { boss.pushedSpeed = 1 * player.pushBack; }
            }
            if (!gameOver && boss.Hp <= 0)
            {
                gameOver = true;
                Destroy(boss.gameObject);
                playerWin = true;
                counter0 = 2;
            }
        }
    }

    void bossCheck()
    {
        bossChecked = true;
        boss.halfTurn();
        //敌人打玩家
        int dir;
        if (boss.thisSpriteRenderer.flipX) { dir = 1; } else { dir = -1; }

        if (boss.attakcing && !player.invicible && player.hurtTurns <= 0 && (((Mathf.Abs(player.position - boss.position) < Mathf.Abs(boss.attackRange * dir))
            && (Mathf.Sign(player.position - boss.position) == Mathf.Sign(boss.attackRange * dir)))
            || player.position == boss.position))
        {
            //print("hitted");
            
            //玩家处于防御状态
            if (player.thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
            {
                //正常成功防御
                if (!(player.position == 3 || player.position == -3))
                {
                    Instantiate(boss.hitEffect2, player.transform.position, transform.rotation);
                    player.turns = 1;
                    player.thisAnim.Play("Charge");
                    player.moveTurns = 1;
                    player.Mp++;
                    if (!boss.facing) { player.speed = -1 * boss.pushBack; }
                    else { player.speed = boss.pushBack; }
                }
                //惨遭崩防
                else
                {
                    Instantiate(boss.hitEffect, player.transform.position, transform.rotation);
                    player.thisAnim.Play("Hurt");
                    player.turns = 3;
                    player.hurtTurns = 3;
                    player.Hp -= boss.damage;
                    effector.hitStun(0.3f);
                    effector.camZoon();
                }
            }

            else
            {
                Instantiate(boss.hitEffect, player.transform.position, transform.rotation);
                effector.hitStun(0.3f);
                effector.camZoon();
                player.attackTurns = 0; player.waitTurns = 0; player.moveTurns = 0; player.attackCounter = 0;
                player.transform.position = new Vector3(player.position * player.moveUnit, player.transform.position.y, player.transform.position.z);
                player.moveTurns = 1;
                if (!boss.facing) { player.speed = -1 * boss.pushBack; }
                else { player.speed = boss.pushBack; }
                player.turns = 3;
                player.hurtTurns = 3;
                player.thisAnim.Play("Hurt");
                player.Hp -= boss.damage;
            }
            player.healthBar.fillAmount = player.Hp / 3;

        }
    }
}
