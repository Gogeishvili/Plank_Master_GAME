using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Scenes
{
    MENU,
    GAMEON,
    WIN,
    LOSS
}


public class UiManager : Singleton<UiManager>
{
    [SerializeField] List<GameObject> _allScenePanel = new List<GameObject>();

    public void LoadScene(Scenes scene)
    {
        foreach (var _scene in _allScenePanel)
        {
            _scene.SetActive(false);
        }

        _allScenePanel[(int)scene].SetActive(true);
    }
}
