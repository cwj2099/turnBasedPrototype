using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthBar_HP;
    public Image HealthBar_Effect;
    float fillAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void healthBar_Update(float givenfillAmount)
    {
        fillAmount = givenfillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar_HP.fillAmount = fillAmount;
        if (HealthBar_Effect.fillAmount > HealthBar_HP.fillAmount)
        {
            HealthBar_Effect.fillAmount -= 10*(HealthBar_Effect.fillAmount-HealthBar_HP.fillAmount) *Time.unscaledDeltaTime;
        }
        else
        {
            HealthBar_Effect.fillAmount = HealthBar_HP.fillAmount;
        }
    }
}
