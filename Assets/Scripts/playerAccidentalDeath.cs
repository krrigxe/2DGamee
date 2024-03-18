using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAccidentalDeath : MonoBehaviour
{
    public GameObject endMenuUi;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           endMenuUi.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Hit");

        }
    }
}
