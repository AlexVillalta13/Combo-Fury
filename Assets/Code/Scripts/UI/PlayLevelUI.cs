using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayLevelUI : UIComponent
{
    Button playButton;

    [SerializeField] GameEvent onPlayButtonPressed;

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        playButton = m_Root.Query<Button>("PlayButton");
    }

    private void OnEnable()
    {
        playButton.clicked += PlayButtonPressed;
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayButtonPressed;
    }

    private void PlayButtonPressed()
    {
        onPlayButtonPressed.Raise();
    }
}
