using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected UIDocument m_Document;
    [SerializeField] protected string elementName;
    const string scaleDownClass = "scaledDown";
    const string scaleUpClass = "scaledUp";

    protected VisualElement m_Root;
    protected VisualElement m_UIElement;

    //[Header("Activation Events")]
    //[SerializeField] GameEvent elementStartedEvent;
    //[SerializeField] GameEvent elementEndedEvent;

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

    public bool IsFlex()
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

    public virtual void SetDisplayElementFlex()
    {
        ShowVisualElement(m_UIElement, true);
        // Activation event
    }

    public virtual void SetDisplayElementNone()
    {
        ShowVisualElement(m_UIElement, false);
        // Deactivation event
    }

    public virtual void SetElementHidden()
    {
        m_UIElement.style.visibility = Visibility.Hidden;
    }

    public virtual void SetElementVisible()
    {
        m_UIElement.style.visibility = Visibility.Visible;
    }

    public virtual void ScaleUpUI()
    {
        VisualElement element = m_UIElement.Q<VisualElement>(className: scaleDownClass);
        element.RemoveFromClassList(scaleDownClass);
        element.AddToClassList(scaleUpClass);
    }

    public virtual void ScaleDownUI()
    {
        VisualElement element = m_UIElement.Q<VisualElement>(className: scaleUpClass);
        element.RemoveFromClassList(scaleUpClass);
        element.AddToClassList(scaleDownClass);
    }

    public void ScaleDownElement(VisualElement element)
    {
        element.AddToClassList(scaleDownClass);
    }

    protected void OnChangeScaleEndEvent(TransitionEndEvent evt)
    {
        foreach(StylePropertyName transitionName in evt.stylePropertyNames)
        {
            if(transitionName == "scale")
            {
                VisualElement element = evt.currentTarget as VisualElement;
                if (element.ClassListContains(scaleUpClass))
                {
                    OnScaledUp();
                }
                else if (element.ClassListContains(scaleDownClass))
                {
                    OnScaledDown();
                }
            }
        }
    }

    public virtual void OnScaledUp() { }
    public virtual void OnScaledDown() { }
}
