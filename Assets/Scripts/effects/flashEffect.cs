﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashEffect : MonoBehaviour
{
    public SpriteRenderer myRenderer;
    public BossController boss;
    public float flashTime = 0.15f;
    public float counter1 = -1;
    public float vibx = 0.5f;
    public float viby = 0.5f;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    // Start is called before the first frame update 
    void Start()
    {
        //myRenderer = gameObject.GetComponent<SpriteRenderer>(); 
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = myRenderer.sharedMaterial.shader;
    }

    // Update is called once per frame 
    void Update()
    {
        if (boss.hitted)
        {
            boss.hitted = false;
            whiteSprite();
            transform.Translate(vibx, viby, 0);
            counter1 = flashTime;
           // print("flash");
        }
        else
        {
            if (counter1 <= 0 && counter1 > -1)
            {
                transform.Translate(-vibx, -viby, 0);
                normalSprite();
                counter1 = -1;
            }
            else if (counter1 == -1)
            {

            }
            else
            {
                counter1 -= Time.deltaTime;
            }
        }
    }

    void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }
}