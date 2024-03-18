using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas=FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {

        charecterEvents.charecterDamaged+=charecterTookDamage;
        charecterEvents.charecterHealed += charecterHealed;
    }
    private void OnDisable()
    {

        charecterEvents.charecterDamaged -= charecterTookDamage;
        charecterEvents.charecterHealed -= charecterHealed;
    }
    public void charecterTookDamage(GameObject charecter,int damageReceived)
    {
        // create text at charecter hit
        Vector3 spawnPosition=Camera.main.WorldToScreenPoint(charecter.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }
    public void charecterHealed(GameObject charecter,int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(charecter.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
}
