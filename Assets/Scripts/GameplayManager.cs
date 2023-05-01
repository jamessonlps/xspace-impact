using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
  [SerializeField] private GameObject playerGO;
  [SerializeField] private GameObject enemy01SpawnerGO;
  [SerializeField] private GameObject enemy03SpawnerGO;
  [SerializeField] private GameObject enemy04SpawnerGO;
  [SerializeField] private GameObject enemy06SpawnerGO;
  // [SerializeField] private GameObject gameOverGO;

  public enum GameplayManagerState
  {
    Starting,
    Phase1,
    Phase2,
    Phase3,
    // GameOver
  }

  GameplayManagerState GPMstate;

  private void Start()
  {
    GPMstate = GameplayManagerState.Starting;
    Invoke("UpdateGameplayState", 0.1f);
  }

  private void UpdateGameplayState()
  {
    switch (GPMstate)
    {
      case GameplayManagerState.Starting:
        Starting();
        break;
      case GameplayManagerState.Phase1:
        Phase1();
        break;
      case GameplayManagerState.Phase2:
        Phase2();
        break;
      case GameplayManagerState.Phase3:
        Phase3();
        break;
        //   case GameplayManagerState.GameOver:
        //     GameOver();
        //     break;
    }
  }

  private void Starting()
  {
    playerGO.SetActive(true);
    SetGameplayManagerState(GameplayManagerState.Phase1);
    Invoke("UpdateGameplayState", 0.1f);
  }


  /// <summary>
  /// Fase 1: Inimigos 1 e 2 (não atiram)
  /// </summary>
  private void Phase1()
  {
    enemy01SpawnerGO.SetActive(true);
    enemy01SpawnerGO.GetComponent<Enemy01Spawner>().ScheduleNextEnemySpawn();
    Invoke("ChangeToPhase2", 30f);
  }


  /// <summary>
  /// Fase 2: Entram os inimigos 3 (que atira)
  /// </summary>
  private void Phase2()
  {
    enemy03SpawnerGO.SetActive(true);
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().ScheduleNextSpawn();
    Invoke("ChangeToPhase3", 60f);
  }


  /// <summary>
  /// Fase 3: Entram os inimigos 4 (persegue o jogador)
  /// </summary>
  private void Phase3()
  {
    enemy04SpawnerGO.SetActive(true);
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().UnscheduleNextEnemySpawn();
  }


  private void GameOver()
  {
    playerGO.SetActive(false);
    enemy01SpawnerGO.SetActive(false);
    enemy03SpawnerGO.SetActive(false);
    enemy04SpawnerGO.SetActive(false);
    enemy06SpawnerGO.SetActive(false);
    // gameOverGO.SetActive(true);
  }


  public void SetGameplayManagerState(GameplayManagerState state)
  {
    GPMstate = state;
  }


  public void ChangeToPhase2()
  {
    SetGameplayManagerState(GameplayManagerState.Phase2);
    UpdateGameplayState();
  }


  public void ChangeToPhase3()
  {
    SetGameplayManagerState(GameplayManagerState.Phase3);
    UpdateGameplayState();
  }
}
