using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseUpgradeUI : UIComponent
{
    [SerializeField] UpgradeInLevelSO upgradeListSO;
    [SerializeField] GameEvent continueWalkingEvent;

    VisualElement holderToScale;
    Button continueButton;
    List<VisualElement> UpgradeContainerList = new List<VisualElement>();
    List<UpgradeInLevelSO.Upgrade> upgradesRandomlySelected = new List<UpgradeInLevelSO.Upgrade>();

    const string upgradeContainerClass = "upgradeContainer";
    const string upgradeNameClass = "nameUpgrade";
    const string upgradeDescriptionClass = "descriptionUpgrade";
    const string scaleDownClass = "scaledDown";
    const string scaleUpClass = "scaledUp";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        holderToScale = m_UIElement.Query<VisualElement>(className: scaleDownClass);
        continueButton = m_UIElement.Query<Button>();
        UpgradeContainerList = m_UIElement.Query<VisualElement>(className: upgradeContainerClass).ToList();

        holderToScale.RegisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);
    }

    private void OnEnable()
    {
        continueButton.clicked += UpgradeSelected;
    }

    private void OnDisable()
    {
        continueButton.clicked -= UpgradeSelected;
    }

    public void UpgradeSelected()
    {
        continueWalkingEvent.Raise();
        ScaleDownUI();
        //HideGameplayElement();
    }



    public void SelectRandomUpgrades()
    {
        upgradesRandomlySelected.Clear();
        int z = 0;
        while(upgradesRandomlySelected.Count < 3)
        {
            z++;
            int i = Random.Range(0, upgradeListSO.UpgradeList.Count);
            UpgradeInLevelSO.Upgrade upgrade = upgradeListSO.UpgradeList[i];
            if (upgradesRandomlySelected.Count == 0)
            {
                upgradesRandomlySelected.Add(upgrade);
            }
            else
            {
                foreach (UpgradeInLevelSO.Upgrade upgradeToCheck in upgradesRandomlySelected)
                {
                    if (upgradeToCheck.UpgradeName == upgrade.UpgradeName)
                    {
                        goto WhileLoop;
                    }
                }

                if (upgrade.CanRepeat == false)
                {
                    // Check unlocked upgrades to see if can appear again
                }
                upgradesRandomlySelected.Add(upgrade);
            }
            WhileLoop:
            continue;
        }

        SetUpgradesUI();
    }

    private void SetUpgradesUI()
    {
        if(upgradesRandomlySelected.Count == 0)
        {
            SelectRandomUpgrades();
        }

        Label upgradeName;
        Label upgradeDescription;

        for(int i = 0; i < upgradesRandomlySelected.Count; i++)
        {
            upgradeName = UpgradeContainerList[i].Query<Label>(className: upgradeNameClass);
            upgradeDescription = UpgradeContainerList[i].Query<Label> (className: upgradeDescriptionClass);

            upgradeName.text = upgradesRandomlySelected[i].UpgradeName;
            upgradeDescription.text = upgradesRandomlySelected[i].UpgradeDescription;
        }
        //RegisterAllEvents();
    }

    private void RegisterAllEvents()
    {
        UpgradeContainerList[0].RegisterCallback<ClickEvent>(RegisterFirstEvent);
        UpgradeContainerList[1].RegisterCallback<ClickEvent>(RegisterSecondEvent);
        UpgradeContainerList[2].RegisterCallback<ClickEvent>(RegisterThirdEvent);
    }

    private void UnregisterAllEvents()
    {
        UpgradeContainerList[0].UnregisterCallback<ClickEvent>(RegisterFirstEvent);
        UpgradeContainerList[1].UnregisterCallback<ClickEvent>(RegisterSecondEvent);
        UpgradeContainerList[2].UnregisterCallback<ClickEvent>(RegisterThirdEvent);
    }

    private void RegisterFirstEvent(ClickEvent evt)
    {
        upgradesRandomlySelected[0].UpgradeEvent.Raise();
        UpgradeSelected();
        UnregisterAllEvents();
    }

    private void RegisterSecondEvent(ClickEvent evt)
    {
        upgradesRandomlySelected[1].UpgradeEvent.Raise();
        UpgradeSelected();
        UnregisterAllEvents();
    }

    private void RegisterThirdEvent(ClickEvent evt)
    {
        upgradesRandomlySelected[2].UpgradeEvent.Raise();
        UpgradeSelected();
        UnregisterAllEvents();
    }

    public override void OnScaledUp()
    {
        base.OnScaledUp();
        RegisterAllEvents();
    }

    public override void OnScaledDown()
    {
        base.OnScaledDown();
        HideGameplayElement();
    }
}
