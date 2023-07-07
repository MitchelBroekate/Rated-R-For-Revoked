using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth;
    public Image bloodVignette = null;
    public Image hurtVignette = null;
    float hurtTimer = 0.1f;
    int maxHealth = 4; 



    void Update()
    {
        for (playerHealth = 4; playerHealth > 0;)
        {
            UpdateHealth();
            StartCoroutine(HurtFlash());
        }

        if (playerHealth <= 0)
        {

        }
    }

    void UpdateHealth()
    {
        Color splatterAlpha = bloodVignette.color;
        splatterAlpha.a = 1 - (playerHealth / maxHealth);
        bloodVignette.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        hurtVignette.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        hurtVignette.enabled = false;
    }
}

