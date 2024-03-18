using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public float countTime = 0;
    public float startingTime = 20f;

    public GameObject endMenuUI;
    public Text CountText;

    public static TimeCount time;

    private void Start()
    {
        time = this;
        countTime = startingTime;
    }
    private void Update()
    {
        countTime-=1*Time.deltaTime;
        CountText.text = countTime.ToString("0");

        if(countTime <= 0)
        {
            endMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
