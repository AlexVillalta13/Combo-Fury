using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] UIDocument gameplayUIDocument;
    VisualElement m_mainMenuRoot;

    [SerializeField] PlayLevelUI playLevelUI;

    List<UIComponent> mainMenuUIElements = new List<UIComponent>();


    private void Awake()
    {
        m_mainMenuRoot = gameplayUIDocument.rootVisualElement;

        mainMenuUIElements.Add(playLevelUI);

        m_mainMenuRoot.style.display = DisplayStyle.Flex;
    }

    public void PlayGame()
    {
        m_mainMenuRoot.style.display = DisplayStyle.None;
    }
}
