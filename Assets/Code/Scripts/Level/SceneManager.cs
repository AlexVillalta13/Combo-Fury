using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScene;
    [SerializeField] SceneElementsHolder alpineWoodsScene;
    [SerializeField] SceneElementsHolder forestScene;

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
        forestScene.gameObject.SetActive(false);
    }

    public void EnableMainMenu()
    {
        DisableAllScenes();
        mainMenuScene.SetActive(true);
    }

    public void ActivateSelectedSceneEnvironment()
    {
        DisableAllScenes();
        switch(currentLevelToLoad.Environment)
        {
            case SceneEnum.AlpineWoods:
                EnableScene(alpineWoodsScene);
                break;
            case SceneEnum.Forest:
                EnableScene(forestScene);
                break;

        }
    }

    private void EnableScene(SceneElementsHolder scene)
    {
        scene.gameObject.SetActive(true);
        scene.SetupLevel(currentLevelToLoad.Enemies.Count);
    }

    private void LoadLevel(LevelSO level)
    {
        currentLevelToLoad = level;
    }
}
