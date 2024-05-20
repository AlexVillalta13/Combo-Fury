using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LevelSelectorUI : UIComponent
{
    [SerializeField] List<LevelSO> m_Levels;
    [SerializeField] LevelSO testLevel;

    const string levelHolderReference = "levelHolder";
    const string backButtonReference = "BackButton";

    List<VisualElement> levelHoldersList = new List<VisualElement>();
    ScrollView scrollView;
    VisualElement backButton;

    public static Action<LevelSO> onSelectedLevelToPlay;
    [SerializeField] UnityEvent onLevelSelectionMenuClosed;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        scrollView = m_UIElement.Query<ScrollView>();
        backButton = m_UIElement.Query<VisualElement>(backButtonReference);
        levelHoldersList = m_UIElement.Query<VisualElement>(className: levelHolderReference).ToList();

        SetLevelImages();
        SetTouchCallbacks();
    }

    private void SetLevelImages()
    {
        foreach (VisualElement levelHolder in levelHoldersList)
        {
            int levelIndex = levelHoldersList.IndexOf(levelHolder);
            VisualElement imageElement = levelHolder.Query<VisualElement>("Image");
            imageElement.style.backgroundImage = new StyleBackground(m_Levels[levelIndex].LevelIcon);
        }
    }

    private void SetTouchCallbacks()
    {
        backButton.RegisterCallback<ClickEvent>(evt => GoBackToPlayLevelScreen());

        foreach (VisualElement levelHolder in levelHoldersList)
        {
            int levelIndex = levelHoldersList.IndexOf(levelHolder);
            levelHolder.RegisterCallback<ClickEvent>(evt => SelectLevelToPlay(levelIndex));
        }
    }
    private void GoBackToPlayLevelScreen()
    {
        onLevelSelectionMenuClosed?.Invoke();
        SetDisplayElementNone();
    }

    private void SelectLevelToPlay(int levelNumber)
    {
        onSelectedLevelToPlay?.Invoke(m_Levels[levelNumber]);
        GoBackToPlayLevelScreen();
    }

    private void Start()
    {
        if(testLevel != null)
        {
            onSelectedLevelToPlay?.Invoke(testLevel);
        }
        else
        {
            onSelectedLevelToPlay?.Invoke(m_Levels[0]);
        }
    }

    public override void SetDisplayElementFlex()
    {
        base.SetDisplayElementFlex();

        StartCoroutine(CenterScrollViewOnVisualElement(levelHoldersList[0]));
    }

    private IEnumerator CenterScrollViewOnVisualElement(VisualElement visualElementToCenterOn)
    {
        yield return null;
        float centerOffset = (scrollView.worldBound.height - visualElementToCenterOn.resolvedStyle.height) / 2;
        scrollView.scrollOffset = new Vector2(0, visualElementToCenterOn.layout.y - centerOffset);
    }
}
