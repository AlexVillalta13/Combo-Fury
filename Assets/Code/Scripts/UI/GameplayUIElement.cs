using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUIElement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected UIDocument m_Document;
    [SerializeField] protected string elementName;

    protected VisualElement m_Root;
    protected VisualElement m_UIElement;

    [Header("Events")]
    [SerializeField] GameEvent elementStartedEvent;
    [SerializeField] GameEvent elementEndedEvent;

    public virtual void OnValidate()
    {
        if(string.IsNullOrEmpty(elementName))
        {
            elementName = this.GetType().Name;
        }
    }

    public virtual void Awake()
    {
        if(m_Document == null)
        {
            m_Document = GetComponentInParent<UIDocument>();
        }

        SetElementsReferences();
    }

    public virtual void SetElementsReferences()
    {
        m_Root = m_Document.rootVisualElement;
        m_UIElement = m_Root.Query<VisualElement>(name: elementName).First();
    }

    public bool IsVisble()
    {
        if(m_UIElement == null)
        {
            return false;
        }

        return (m_UIElement.style.display == DisplayStyle.Flex);
    }

    public VisualElement GetVisualElement(string elementName)
    {
        if(string.IsNullOrEmpty(elementName))
        {
            return null;
        }

        return m_Root.Q(name: elementName);
    }

    public static void ShowVisualElement(VisualElement visualElement, bool state)
    {
        if(visualElement == null)
        {
            return;
        }

        visualElement.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public virtual void ShowGameplayElement()
    {
        ShowVisualElement(m_UIElement, true);
        // Activation event
    }

    public virtual void HideGameplayElement()
    {
        ShowVisualElement(m_UIElement, false);
        // Deactivation event
    }
}
