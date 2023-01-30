using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayLevelUI : UIComponent
{
    Button playButton;
    const string playButtonReference = "PlayButton";

    VisualElement levelSelectedImage;
    const string levelSelectorImageReference = "LevelSelectedContainer";

    [SerializeField] GameEvent onPlayButtonPressedEvent;
    [SerializeField] GameEvent onOpenLevelSelectorEvent;

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
        onPlayButtonPressedEvent.Raise();
    }

    private void OpenLevelSelector(ClickEvent evt)
    {
        onOpenLevelSelectorEvent.Raise();
    }
}
