using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject GameOverPanel;
    public GameObject GameFinishedPanel;
    public GameObject StartButton;
    public GameObject ResetGameButton;
    public GameObject DeathsCounter;

    void Start()
    {
        if (!GameManager.instance.isNewGame)
        {
            StartButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Continue";
            ResetGameButton.SetActive(true);
        }
        else
        {
            StartButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Start";

        }
    }

    public void ShowGameFinishedPanel() {
        MainMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameFinishedPanel.SetActive(true);
    }

    public void ShowGameOverPanel(int deaths)
    {
        DeathsCounter.GetComponent<TMP_Text>().text = "Deaths: " + deaths;
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
