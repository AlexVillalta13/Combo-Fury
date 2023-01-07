using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScene;
    [SerializeField] GameObject alpineWoodsScene;

    private void Start()
    {
        EnableMainMenu();
    }

    public void DisableAllScenes()
    {
        mainMenuScene.SetActive(false);
        alpineWoodsScene.SetActive(false);
    }

    public void EnableMainMenu()
    {
        DisableAllScenes();
        mainMenuScene.SetActive(true);
    }

    public void EnableAlpineWoodsScene()
    {
        DisableAllScenes();
        alpineWoodsScene.SetActive(true);
    }
}
