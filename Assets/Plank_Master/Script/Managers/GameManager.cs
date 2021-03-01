using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool gameOn = false;

    [SerializeField] List<Character> _players = new List<Character>();

    private void Awake()
    {
        InitGame();
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
        gameOn = false;
        UiManager.instance.LoadScene(Scenes.LOSS);
    }
    public void GameWin()
    {
        gameOn=false;
        UiManager.instance.LoadScene(Scenes.WIN);
    }

    public void UpdatePlayerInfo(Character player)
    {

        if (player.isPlayer)
        {
            GameLoss();
        }
       
        _players.Remove(player);

         if (_players.Count == 1 && _players[0].isPlayer)
        {
            GameWin();
        }

    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

}
