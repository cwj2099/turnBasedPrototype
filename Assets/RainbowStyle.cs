using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowStyle : MonoBehaviour
{
    public SpriteRenderer thisSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisSpriteRenderer.material.SetColor("_Color1", new Color(Random.value, Random.value, Random.value));
        thisSpriteRenderer.material.SetColor("_Color2", new Color(Random.value, Random.value, Random.value));
        thisSpriteRenderer.material.SetColor("_Color3", new Color(Random.value, Random.value, Random.value));
        thisSpriteRenderer.material.SetColor("_Color4", new Color(Random.value, Random.value, Random.value));
    }
}
