using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class countDOwn : MonoBehaviour
{
    public Text countText;
    public float count = 10;
    public GameObject endMenuUi;

    // Update is called once per frame
    void Update()
    {
        count -= .01f;

        countText.text = count.ToString("0");

        if (count <= 0)
        {
            endMenuUi.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
