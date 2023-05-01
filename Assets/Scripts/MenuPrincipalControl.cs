using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalControl : MonoBehaviour
{
    [SerializeField] private string nameGame;

    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject painelOptions2;
    [SerializeField] private GameObject painelBack;
    [SerializeField] private GameObject painelNext;
    [SerializeField] private GameObject painelBack2;

    public void Play()
    {
        SceneManager.LoadScene(nameGame);
    }

    public void OpenOptions1()
    {
        painelMenu.SetActive(false);
        painelOptions.SetActive(true);
        painelOptions2.SetActive(false);
        painelBack.SetActive(true);
        painelNext.SetActive(true);
        painelBack2.SetActive(false);
    }

    public void LeaveOptions1()
    {
        painelMenu.SetActive(true);
        painelOptions.SetActive(false);
        painelOptions2.SetActive(false);
        painelBack.SetActive(false);
        painelNext.SetActive(false);
        painelBack2.SetActive(false);
    }

    public void OpenOptions2()
    {
        painelMenu.SetActive(false);
        painelOptions.SetActive(false);
        painelOptions2.SetActive(true);
        painelBack.SetActive(false);
        painelNext.SetActive(false);
        painelBack2.SetActive(true);
    }

    public void LeaveOptions2()
    {
        painelMenu.SetActive(false);
        painelOptions.SetActive(true);
        painelOptions2.SetActive(false);
        painelBack.SetActive(true);
        painelNext.SetActive(true);
        painelBack2.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}