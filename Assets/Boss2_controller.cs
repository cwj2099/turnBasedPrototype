using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_controller : BossController
{

    // Update is called once per frame
    public override void Update()
    {
        if (turns <=0 && !GM.gameOver)
        {
            /* if (player.position < position) { facing = false; }
             else if (player.position > position) { facing = true; }
             thisSpriteRenderer.flipX = facing;*/
            thisSpriteRenderer.flipX = player.thisSpriteRenderer.flipX;
            facing = thisSpriteRenderer.flipX;
        }

        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3Pre"))
        {
            if (waitTurns == 1)
            {
                if (thisSpriteRenderer.flipX) { speed = player.position - position; }
                else { speed = player.position - position; }
            }
            if (waitTurns == 0)
            {
                if (transform.position.y < 5) { transform.Translate(0, 50 * Time.deltaTime, 0); }
            }
        }

        if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack3Pro"))
        {
            if (turns == 4)
            {
                //waitTurns = 2;
                attackTurns = 2;
                attackRange = 1;
            }
            if (transform.position.y > 0) { transform.Translate(0, -50 * Time.deltaTime, 0); }
        }

        if (turns == 0)
        {


            if (Mathf.Abs(player.position - position) >= 3)
            {
                thisAnim.Play("Attack3Pre");
                turns = 8;
                waitTurns = 3;
                moveTurns = 1;
                pushBack = 2;
                invicibleTurns = 2;
            }

            else if (position%2==0) {
                thisAnim.Play("Attack2Pre");
                turns = 12;
                waitTurns = 5;
                moveTurns = 5;
                attackTurns = 5;
                attackRange = -2;
                pushBack = 1;
                if (thisSpriteRenderer.flipX) { speed = 1; } else { speed = -1; }
            }
            else 
            {
                thisAnim.Play("Attack1Pre");
                turns = 8;
                waitTurns = 3;
                attackTurns = 1;
                moveTurns = 1;
                pushBack = 2;
                if (thisSpriteRenderer.flipX) { speed = 1; } else { speed = -1; }
                attackRange = 2;
            }

            
        }

        

    }

}
