using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseUpgradeUI : UIComponent
{
    [Header("Upgrades Lists SO")]
    [SerializeField] UpgradeInLevelSO upgradeListSO;
    [SerializeField] UpgradeInLevelSO upgradesPickupSO;
    [Header("Events")]
    [SerializeField] GameEvent continueWalkingEvent;

    VisualElement holderToScale;
    List<VisualElement> UpgradeContainerList = new List<VisualElement>();
    List<UpgradeInLevelSO.Upgrade> upgradesRandomlySelected = new List<UpgradeInLevelSO.Upgrade>();

    const string upgradeContainerClass = "upgradeContainer";
    const string upgradeNameClass = "nameUpgrade";
    const string upgradeDescriptionClass = "descriptionUpgrade";
    const string scaleHolderElement = "HolderToScale";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        holderToScale = m_UIElement.Query<VisualElement>(name: scaleHolderElement);
        UpgradeContainerList = m_UIElement.Query<VisualElement>(className: upgradeContainerClass).ToList();

        holderToScale.RegisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);
    }

    public void UpgradeSelected()
    {
        continueWalkingEvent.Raise();
        ScaleDownUI();
    }

    public void SelectRandomUpgrades()
    {
        upgradesRandomlySelected.Clear();

        while(upgradesRandomlySelected.Count < 3)
        {
            int i = Random.Range(0, upgradeListSO.UpgradeList.Count);
            UpgradeInLevelSO.Upgrade upgrade = upgradeListSO.UpgradeList[i];

            if (upgrade.CanRepeat == false)
            {
                // Check unlocked upgrades to see if can appear again
                foreach (UpgradeInLevelSO.Upgrade upgradeToCheck in upgradesPickupSO.UpgradeList)
                {
                    if (upgradeToCheck.UpgradeName == upgrade.UpgradeName)
                    {
                        goto WhileLoop;
                    }
                }
            }

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
        upgradesPickupSO.UpgradeList.Add(upgradesRandomlySelected[0]);
        UpgradeSelected();
        UnregisterAllEvents();
    }

    private void RegisterSecondEvent(ClickEvent evt)
    {
        upgradesRandomlySelected[1].UpgradeEvent.Raise();
        upgradesPickupSO.UpgradeList.Add(upgradesRandomlySelected[1]);
        UpgradeSelected();
        UnregisterAllEvents();
    }

    private void RegisterThirdEvent(ClickEvent evt)
    {
        upgradesRandomlySelected[2].UpgradeEvent.Raise();
        upgradesPickupSO.UpgradeList.Add(upgradesRandomlySelected[2]);
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
