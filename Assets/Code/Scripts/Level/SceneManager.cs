using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScene;
    [SerializeField] SceneElementsHolder alpineWoodsScene;

    [SerializeField] LevelSO currentLevelToLoad;

    private void OnEnable()
    {
        LevelSelectorUI.loadLevel += LoadLevel;
    }

    private void OnDisable()
    {
        LevelSelectorUI.loadLevel -= LoadLevel;
    }

    private void Start()
    {
        EnableMainMenu();
    }

    public void DisableAllScenes()
    {
        mainMenuScene.SetActive(false);
        alpineWoodsScene.gameObject.SetActive(false);
    }

    public void EnableMainMenu()
    {
        DisableAllScenes();
        mainMenuScene.SetActive(true);
    }

    public void EnableAlpineWoodsScene()
    {
        DisableAllScenes();
        alpineWoodsScene.gameObject.SetActive(true);
        alpineWoodsScene.SetupLevel(currentLevelToLoad.Enemies.Count);
    }

    private void LoadLevel(LevelSO level)
    {
        currentLevelToLoad = level;
    }
}
