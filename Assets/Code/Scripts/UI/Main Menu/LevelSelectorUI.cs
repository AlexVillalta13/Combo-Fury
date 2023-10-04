using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectorUI : UIComponent
{
    [SerializeField] List<LevelSO> m_Levels;
    [SerializeField] LevelSO testLevel;

    List<VisualElement> levelContainersList = new List<VisualElement>();

    const string levelHolderReference = "levelHolder";

    public static Action<LevelSO> loadLevel;
    [SerializeField] GameEvent levelSelected;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        levelContainersList = m_UIElement.Query<VisualElement>(className: levelHolderReference).ToList();
        SetTouchCallbacks();
    }

    private void Start()
    {
        if(testLevel != null)
        {
            loadLevel?.Invoke(testLevel);
        }
        else
        {
            loadLevel?.Invoke(m_Levels[0]);
        }
    }

    private void SetTouchCallbacks()
    {
        levelContainersList[0].RegisterCallback<ClickEvent>(LoadFirstLevelCallback);
        levelContainersList[1].RegisterCallback<ClickEvent>(LoadSecondLevelCallback);
        levelContainersList[2].RegisterCallback<ClickEvent>(LoadThirdLevelCallback);
    }

    private void LoadFirstLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[0]);
        levelSelected.Raise(gameObject);
        HideGameplayElement();
    }
    private void LoadSecondLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[1]);
        levelSelected.Raise(gameObject);
        HideGameplayElement();
    }
    private void LoadThirdLevelCallback(ClickEvent evt)
    {
        loadLevel?.Invoke(m_Levels[2]);
        levelSelected.Raise(gameObject);
        HideGameplayElement();
    }
}
