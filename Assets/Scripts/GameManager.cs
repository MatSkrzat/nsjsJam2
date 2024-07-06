using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public UIManager ui;
    public GameObject player;
    public GameObject king;
    public bool isGameStarted = false;
    public bool isGameOver = false;
    PlayerBehaviour playerBehaviour;
    KingBehaviour kingBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        kingBehaviour = king.GetComponent<KingBehaviour>();
    }

    public void StartGame()
    {
        isGameStarted = true;
        playerBehaviour.StartMoving();
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void ReloadGame()
    {
        CallOnReload();
        SceneManager.LoadScene(0);
    }

    void CallOnReload()
    {
        isGameOver = false;
        isGameStarted = false;
    }
}
