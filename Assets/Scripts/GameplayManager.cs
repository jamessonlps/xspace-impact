using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
  [SerializeField] private GameObject playerGO;
  [SerializeField] private Timer timer;

  [Header("Enemy Spawners")]
  [SerializeField] private GameObject enemy01SpawnerGO;
  [SerializeField] private GameObject enemy03SpawnerGO;
  [SerializeField] private GameObject enemy04SpawnerGO;
  [SerializeField] private GameObject enemy06SpawnerGO;

  [Header("User Interface")]
  [SerializeField] private GameObject gameOverGO;
  [SerializeField] private GameObject gameRunningGO;

  [Header("Collectable Spawner")]
  [SerializeField] private GameObject collectableSpawnerGO;

  public enum GameplayManagerState
  {
    Starting,
    Phase1,
    Phase2,
    Phase3,
    Phase4,
    Phase5,
    Phase6,
    GameOver
  }

  GameplayManagerState GPMstate;

  private void Start()
  {
    gameOverGO.SetActive(false);
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
      case GameplayManagerState.Phase4:
        Phase4();
        break;
      case GameplayManagerState.Phase5:
        Phase5();
        break;
      case GameplayManagerState.Phase6:
        Phase6();
        break;
      case GameplayManagerState.GameOver:
        GameOver();
        break;
      default:
        Starting();
        break;
    }
  }

  private void Starting()
  {
    timer.StartTimer();
    SetGameplayManagerState(GameplayManagerState.Phase1);
    Invoke("UpdateGameplayState", 0.1f);
  }


  private void Phase1()
  {
    enemy01SpawnerGO.GetComponent<Enemy01Spawner>().ScheduleNextEnemySpawn();
    Invoke("ChangeToPhase2", 30f);
  }


  private void Phase2()
  {
    collectableSpawnerGO.GetComponent<CollectableSpawner>().SpawnLifeCollectable();
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().SpawnEnemy();
    Invoke("ChangeToPhase3", 60f);
  }


  private void Phase3()
  {
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().UnscheduleNextEnemySpawn();
    enemy06SpawnerGO.GetComponent<Enemy06Spawner>().SpawnEnemy();
    Invoke("ChangeToPhase4", 60f);
  }


  private void Phase4()
  {
    enemy06SpawnerGO.GetComponent<Enemy03Spawner>().UnscheduleNextEnemySpawn();
    enemy04SpawnerGO.GetComponent<Enemy04Spawner>().SpawnEnemy();
    Invoke("ChangeToPhase5", 60f);
  }


  private void Phase5()
  {
    enemy06SpawnerGO.GetComponent<Enemy03Spawner>().SpawnEnemy();
    Invoke("ChangeToPhase6", 60f);
  }


  private void Phase6()
  {
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().SpawnEnemy();
  }


  private void GameOver()
  {
    timer.StopTimer();
    CancelAllInvokes();
    UnscheduleAllSpawners();
    gameRunningGO.SetActive(false);
    Invoke("ShowGameOver", 3f);
  }

  private void ShowGameOver()
  {
    gameOverGO.SetActive(true);
    Invoke("BackToMenu", 3f);
  }

  public void BackToMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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


  public void ChangeToPhase4()
  {
    SetGameplayManagerState(GameplayManagerState.Phase4);
    UpdateGameplayState();
  }


  public void ChangeToPhase5()
  {
    SetGameplayManagerState(GameplayManagerState.Phase5);
    UpdateGameplayState();
  }


  public void ChangeToPhase6()
  {
    SetGameplayManagerState(GameplayManagerState.Phase6);
    UpdateGameplayState();
  }


  public void ChangeToGameOver()
  {
    SetGameplayManagerState(GameplayManagerState.GameOver);
    UpdateGameplayState();
  }


  private void CancelAllInvokes()
  {
    CancelInvoke("ChangeToPhase2");
    CancelInvoke("ChangeToPhase3");
    CancelInvoke("ChangeToPhase4");
    CancelInvoke("ChangeToPhase5");
    CancelInvoke("ChangeToPhase6");
  }

  private void UnscheduleAllSpawners()
  {
    enemy01SpawnerGO.GetComponent<Enemy01Spawner>().UnscheduleEnemySpawner();
    enemy03SpawnerGO.GetComponent<Enemy03Spawner>().UnscheduleNextEnemySpawn();
    enemy04SpawnerGO.GetComponent<Enemy04Spawner>().UnscheduleNextSpawnEnemy();
    enemy06SpawnerGO.GetComponent<Enemy06Spawner>().UnscheduleNextSpawn();
    collectableSpawnerGO.GetComponent<CollectableSpawner>().UnscheduleLifeCollectableSpawn();
  }
}
