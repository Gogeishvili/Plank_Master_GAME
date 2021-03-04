using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool gameOn = false;

    [SerializeField] List<Character> _Characters = new List<Character>();

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
    void GameLoss()
    {
        gameOn = false;
        UiManager.instance.LoadScene(Scenes.LOSS);
    }
    void GameWin()
    {
        gameOn = false;
        UiManager.instance.LoadScene(Scenes.WIN);
    }

    public void UpdatePlayerInfo(Character player)
    {

        if (player.isPlayer)
        {
            GameLoss();
        }

        _Characters.Remove(player);

        //game win
        //  if (_Characters.Count == 1 && _Characters[0].isPlayer)
        // {
        //     GameWin();
        // }

    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

}
