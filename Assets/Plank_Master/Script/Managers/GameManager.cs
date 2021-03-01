using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool gameOn = false;

    [SerializeField] List<Character> _players = new List<Character>();

    private void Awake()
    {

    }

    void InitGame()
    {
        gameOn = false;
        UiManager.instance.LoadScene(Scenes.MENU);
    }
    public void GameOn()
    {
        gameOn = true;
        UiManager.instance.LoadScene(Scenes.GAMEON);
    }
    public void GameLoss()
    {

    }
    public void GameWin()
    {

    }

}
