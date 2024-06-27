using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuTagsUI : UIComponent
{
    const string screenHolderScrollerReference = "MainMenuScreensHolderScroller";
    const string movingIconBackgroundReference = "MovingIconBackground";
    const string shopButtonReference = "ShopButton";
    const string inventoryButtonReference = "InventoryButton";
    const string playLevelButtonReference = "PlayLevelButton";
    const string upgradesButtonReference = "UpgradesButton";

    VisualElement mainMenuScreenHolderScroller;
    VisualElement movingIconBackground;
    VisualElement shopButton;
    VisualElement inventoryButton;
    VisualElement playLevelButton;
    VisualElement upgradesButton;

    const float shopScreenPercentagePosition = 0f;
    const float movingBackgroundOnShopPosition = 0f;
    const float inventoryScreenPercentagePosition = 100f;
    const float movingBackgroundOnInventoryPosition = 20f;
    const float playLevelScreenPercentagePosition = 200f;
    const float movingBackgroundOnPlayLevelPosition = 40f;
    const float upgradeScreenPercentagePosition = 300f;
    const float movingBackgroundOnUpgradesPosition = 60f;


    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        mainMenuScreenHolderScroller = m_Root.Query<VisualElement>(screenHolderScrollerReference);
        movingIconBackground = m_UIElement.Query<VisualElement>(movingIconBackgroundReference);

        shopButton = m_UIElement.Query<VisualElement>(shopButtonReference);
        inventoryButton = m_UIElement.Query<VisualElement>(inventoryButtonReference);
        playLevelButton = m_UIElement.Query<VisualElement>(playLevelButtonReference);
        upgradesButton = m_UIElement.Query<VisualElement>(upgradesButtonReference);

        SetTouchCallbacks();
    }

    private void SetTouchCallbacks()
    {
        shopButton.RegisterCallback<ClickEvent>( evt => MoveMainMenuScrollerToPercentagePosition(shopScreenPercentagePosition, movingBackgroundOnShopPosition) );
        inventoryButton.RegisterCallback<ClickEvent>( evt => MoveMainMenuScrollerToPercentagePosition(inventoryScreenPercentagePosition, movingBackgroundOnInventoryPosition) );
        playLevelButton.RegisterCallback<ClickEvent>( evt => MoveMainMenuScrollerToPercentagePosition(playLevelScreenPercentagePosition, movingBackgroundOnPlayLevelPosition) );
        upgradesButton.RegisterCallback<ClickEvent>( evt => MoveMainMenuScrollerToPercentagePosition(upgradeScreenPercentagePosition, movingBackgroundOnUpgradesPosition) );
    }

    private void MoveMainMenuScrollerToPercentagePosition(float positionPercentageToMove, float movingBackgroundPosition)
    {
        mainMenuScreenHolderScroller.style.right = new Length(positionPercentageToMove, LengthUnit.Percent);
        movingIconBackground.style.left = new Length(movingBackgroundPosition, LengthUnit.Percent);
    }
}
