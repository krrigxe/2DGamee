using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    damagable playerDamagable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
     
        if(player== null)
        {
            Debug.Log("No player found in the scene. make sure it has tag Player");
        }

        playerDamagable = player.GetComponent<damagable>();
    }
    // Start is called before the first frame update
    void Start() 
    {
        healthSlider.value = CalculateSliderPersentage(playerDamagable.Health, playerDamagable.maxHealth);
        healthBarText.text = "HP " + playerDamagable.Health + " / " + playerDamagable.maxHealth;
    }
    private void OnEnable()
    {
        playerDamagable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamagable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculateSliderPersentage(float currentHealth , float MaxHealth)
    {
        return currentHealth / MaxHealth;
    }

  private void OnPlayerHealthChanged(int newHealth, int MaxHealth)
    {
        healthSlider.value = CalculateSliderPersentage(newHealth, MaxHealth);
        healthBarText.text = "HP " + newHealth + " / " + MaxHealth;
    }
}
