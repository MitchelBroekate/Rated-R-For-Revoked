using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Player Health Amount")]
    public float currentHealth = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private int regenRate = 1;
    private bool canRegen = false;

    [Header("Blood Vignette")]
    [SerializeField] private Image bloodVignette = null;

    [Header("Heal Timer")]
    [SerializeField] private float healCooldown = 3.0f;
    [SerializeField] private float maxHealCooldown = 3.0f;
    [SerializeField] private bool startCooldown = false;

    [Header("Hurt Vignette")]
    [SerializeField] private Image hurtVignette = null;
    [SerializeField] private float hurtTimer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if (healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
        }

        if (canRegen)
        {
            if (currentHealth <= maxHealth - 0.01)
            {
                currentHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else
            {
                currentHealth = maxHealth;
                healCooldown = maxHealCooldown;
                canRegen = false;
            }
        }
    }

    void UpdateHealth()
    {
        Color splatterAlpha = bloodVignette.color;
        splatterAlpha.a = 1 - (currentHealth / maxHealth);
        bloodVignette.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        hurtVignette.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        hurtVignette.enabled = false;
    }

    public void TakeDamage()
    {
        if (currentHealth >= 0)
        {
            canRegen = false;
            StartCoroutine(HurtFlash());
            UpdateHealth();
            healCooldown = maxHealCooldown;
            startCooldown = true;
        }
    }
}
