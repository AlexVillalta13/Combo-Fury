using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayLevelUI : UIComponent
{
    const string playButtonReference = "PlayButton";
    const string levelSelectedContainerImageReference = "LevelSelectedContainer";
    const string levelSelectedImageReference = "LevelImage";
    const string levelSelectedNameReference = "LevelName";

    Button playButton;
    VisualElement levelImageContainer;
    VisualElement levelImage;
    Label levelSelectedName;

    [SerializeField] UnityEvent openLevelSelector;

    [SerializeField] GameEvent onPlayButtonPressedEvent;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        playButton = m_Root.Query<Button>(name: playButtonReference);
        levelImageContainer = m_Root.Query<VisualElement>(name: levelSelectedContainerImageReference);
        levelImage =levelImageContainer.Query<VisualElement>(levelSelectedImageReference);
        levelSelectedName = levelImageContainer.Query<Label>(levelSelectedNameReference);
    }

    private void OnEnable()
    {
        playButton.clicked += PlayButtonPressed;
        levelImageContainer.RegisterCallback<ClickEvent>(OpenLevelSelector);
        LevelSelectorUI.onSelectedLevelToPlay += ChangeLevelSelectedImageAndName;
    }

    private void ChangeLevelSelectedImageAndName(LevelSO level)
    {
        levelImage.style.backgroundImage = new StyleBackground(level.LevelIcon);
        levelSelectedName.text = level.name;
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayButtonPressed;
        levelImageContainer.UnregisterCallback<ClickEvent>(OpenLevelSelector);
    }

    private void PlayButtonPressed()
    {
        onPlayButtonPressedEvent.Raise(gameObject);
    }

    private void OpenLevelSelector(ClickEvent evt)
    {
        openLevelSelector?.Invoke();
    }

    public void SetFocus(bool state)
    {
        m_UIElement.focusable = state;
    }
}
