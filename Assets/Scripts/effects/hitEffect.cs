using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffect : MonoBehaviour
{
    public Animator thisAnimator;
    public List<ParticleSystem> toFlip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Destroy(gameObject);
        }
    }

    public void flipX()
    {
        foreach(ParticleSystem pt in toFlip)
        {
            var temp = pt.main;
            temp.flipRotation = 1;
        }
    }
}
