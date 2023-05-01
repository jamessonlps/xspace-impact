using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalControl : MonoBehaviour
{
    [SerializeField] private string nameGame;

    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject painelBack;

    public void Play()
    {
        SceneManager.LoadScene(nameGame);
    }

    public void OpenOptions()
    {
        painelMenu.SetActive(false);
        painelOptions.SetActive(true);
        painelBack.SetActive(true);
    }

    public void LeaveOptions()
    {
        painelMenu.SetActive(true);
        painelOptions.SetActive(false);
        painelBack.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}