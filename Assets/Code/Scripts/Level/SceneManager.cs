using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScene;
    [SerializeField] SceneElementsHolder alpineWoodsScene;
    [SerializeField] SceneElementsHolder forestScene;

    ILevelData currentLevelToLoad;

    private void OnEnable()
    {
        LevelSelectorUI.onSelectedLevelToPlay += LoadLevel;
    }

    private void OnDisable()
    {
        LevelSelectorUI.onSelectedLevelToPlay -= LoadLevel;
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
        switch(currentLevelToLoad.GetEnvironment())
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
        scene.SetupLevel(currentLevelToLoad.GetTotalEnemiesCount());
    }

    private void LoadLevel(ILevelData level)
    {
        currentLevelToLoad = level;
    }
}
