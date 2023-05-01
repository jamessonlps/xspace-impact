using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
  TMP_Text timerText;
  float gameplayTime;

  void Start()
  {
    timerText = GetComponent<TMP_Text>();
    gameplayTime = 0;
  }

  void Update()
  {
    gameplayTime += Time.deltaTime;

    int hours = (int)gameplayTime / 3600;
    int minutes = ((int)gameplayTime / 60) % 60;
    int secs = (int)gameplayTime % 60;

    timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + secs.ToString("00");
  }
}
