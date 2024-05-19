using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayLevelUI : UIComponent
{
    Button playButton;
    const string playButtonReference = "PlayButton";

    VisualElement levelSelectedImage;
    const string levelSelectorImageReference = "LevelSelectedContainer";

    [SerializeField] UnityEvent openLevelSelector;

    [SerializeField] GameEvent onPlayButtonPressedEvent;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        playButton = m_Root.Query<Button>(name: playButtonReference);
        levelSelectedImage = m_Root.Query<VisualElement>(name: levelSelectorImageReference);
    }

    private void OnEnable()
    {
        playButton.clicked += PlayButtonPressed;
        levelSelectedImage.RegisterCallback<ClickEvent>(OpenLevelSelector);
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayButtonPressed;
        levelSelectedImage.UnregisterCallback<ClickEvent>(OpenLevelSelector);
    }

    private void PlayButtonPressed()
    {
        onPlayButtonPressedEvent.Raise(gameObject);
    }

    private void OpenLevelSelector(ClickEvent evt)
    {
        openLevelSelector?.Invoke();

    }
}
