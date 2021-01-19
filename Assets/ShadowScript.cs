using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public SpriteRenderer bossSprite;
    public SpriteRenderer thisSprite;
    public BossController boss;

    public Color dangerColor;
    public Color attackColor;
    public Color safeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisSprite.sprite = bossSprite.sprite;
        thisSprite.flipX = bossSprite.flipX;
        if (boss.waitTurns > 0) { thisSprite.color = dangerColor; }
        else if (boss.attakcing) { thisSprite.color = attackColor; }
        else { thisSprite.color = safeColor; }
    }
}
