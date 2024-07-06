using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject GameOverPanel;

    public void ShowGameOverPanel()
    {
        MainMenuPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }


    public void OnStartButtonClick()
    {
        MainMenuPanel.SetActive(false);
        GameManager.instance.StartGame();
    }

    public void OnTryAgainButtonClick()
    {
        GameManager.instance.ReloadGame();
    }
}
