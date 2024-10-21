using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SceneTransitionUI : UIComponent
{
    private const string blackBackgroundName = "BlackBackground";
    private VisualElement blackBackground;

    [SerializeField] private GameEvent OnChangeScene;
    [SerializeField] private GameEvent OnLevelLoadedEvent;
    public override void SetElementsReferences()
    {
        base.SetElementsReferences();
        blackBackground = m_UIElement.Query<VisualElement>(name: blackBackgroundName);
        blackBackground.RegisterCallback<TransitionEndEvent>(OnChangeOpacityEvent);
        SetDisplayElementNone();
    }

    public void SetOpacity100()
    {
        SetDisplayElementFlex();
        blackBackground.RemoveFromClassList(opacity0Class);
    }

    public void SetOpacity0()
    {
        blackBackground.AddToClassList(opacity0Class);
        OnLevelLoadedEvent?.Raise(this);
    }

    private void OnChangeOpacityEvent(TransitionEndEvent evt)
    {
        if (blackBackground.ClassListContains(opacity0Class))
        {
            OnOpacity0();
        }
        else
        {
            OnOpacity100();
        }
    }

    private void OnOpacity100()
    {
        Debug.Log("Opacity100");

        OnChangeScene?.Raise(this);
        SetOpacity0();
    }

    private void OnOpacity0()
    {
        SetDisplayElementNone();
        Debug.Log("Opacity0");
    }
}
