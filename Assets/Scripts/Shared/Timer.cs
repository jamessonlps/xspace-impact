using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
  [SerializeField] private TMP_Text totalTimeText;
  [SerializeField] private TMP_Text currentTimeText;

  public bool isTimerRunning = false;

  float gameplayTime;

  public void StartTimer()
  {
    gameplayTime = 0f;
    isTimerRunning = true;
  }

  public void StopTimer()
  {
    isTimerRunning = false;
  }

  private void Update()
  {
    if (isTimerRunning)
      UpdateTimerUI();
    else
      UpdateTotalTimeUI();
  }

  private void UpdateTimerUI()
  {
    gameplayTime += Time.deltaTime;

    int hours = (int)gameplayTime / 3600;
    int minutes = ((int)gameplayTime / 60) % 60;
    int secs = (int)gameplayTime % 60;

    currentTimeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + secs.ToString("00");
  }

  private void UpdateTotalTimeUI()
  {
    int hours = (int)gameplayTime / 3600;
    int minutes = ((int)gameplayTime / 60) % 60;
    int secs = (int)gameplayTime % 60;

    totalTimeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + secs.ToString("00");
  }
}
