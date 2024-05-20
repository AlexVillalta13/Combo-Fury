using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseUpgradeUI : UIComponent
{
    [Header("Upgrades Lists SO")]
    [SerializeField] UpgradeInLevelSO upgradeListSO;
    [SerializeField] UpgradeInLevelSO upgradesPlayerHasSO;
    [Header("Events")]
    [SerializeField] GameEvent continueWalkingEvent;
    [SerializeField] GameEvent upgradeChoosed;

    VisualElement holderToScale;
    List<VisualElement> UpgradeContainerList = new List<VisualElement>();

    [SerializeField] List<Upgrade> upgradesThatCanBeSelected = new List<Upgrade>();
    List<Upgrade> upgradesRandomlySelected = new List<Upgrade>();

    const string upgradeContainerClass = "upgradeContainer";
    const string upgradeNameClass = "nameUpgrade";
    const string iconImageName = "IconImage";
    const string upgradeDescriptionClass = "descriptionUpgrade";
    const string scaleHolderElement = "HolderToScale";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        holderToScale = m_UIElement.Query<VisualElement>(name: scaleHolderElement);
        UpgradeContainerList = m_UIElement.Query<VisualElement>(className: upgradeContainerClass).ToList();

        holderToScale.RegisterCallback<TransitionEndEvent>(OnChangeScaleEndEvent);

        upgradesThatCanBeSelected = new List<Upgrade>(upgradeListSO.UpgradeList);
    }

    public void ResetUpgradesCanChooseList()
    {
        upgradesThatCanBeSelected = new List<Upgrade>(upgradeListSO.UpgradeList);
    }

    public void UpgradeSelected()
    {
        continueWalkingEvent.Raise(gameObject);
        ScaleDownUI();
        upgradeChoosed.Raise(gameObject);
    }

    public void SelectRandomUpgrades()
    {
        upgradesRandomlySelected.Clear();

        while(upgradesRandomlySelected.Count < 3)
        {
            int i = Random.Range(0, upgradesThatCanBeSelected.Count);
            Upgrade upgrade = upgradesThatCanBeSelected[i];

            if (upgradesRandomlySelected.Count == 0)
            {
                upgradesRandomlySelected.Add(upgrade);
            }
            else
            {
                foreach (Upgrade upgradeToCheck in upgradesRandomlySelected)
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
        VisualElement iconImage;
        Label upgradeDescription;

        for(int i = 0; i < upgradesRandomlySelected.Count; i++)
        {
            upgradeName = UpgradeContainerList[i].Query<Label>(className: upgradeNameClass);
            iconImage = UpgradeContainerList[i].Query<VisualElement>(name: iconImageName);
            upgradeDescription = UpgradeContainerList[i].Query<Label> (className: upgradeDescriptionClass);

            upgradeName.text = upgradesRandomlySelected[i].UpgradeName;
            iconImage.style.backgroundImage = new StyleBackground(upgradesRandomlySelected[i].Image);
            upgradeDescription.text = upgradesRandomlySelected[i].UpgradeDescription;
        }
    }

    private void RegisterAllEvents()
    {
        UpgradeContainerList[0].RegisterCallback<ClickEvent>(OnFirstUpgradeSelectes);
        UpgradeContainerList[1].RegisterCallback<ClickEvent>(OnSecondUpgradeSelected);
        UpgradeContainerList[2].RegisterCallback<ClickEvent>(OnThirdUpgradeSelected);
    }

    private void UnregisterAllEvents()
    {
        UpgradeContainerList[0].UnregisterCallback<ClickEvent>(OnFirstUpgradeSelectes);
        UpgradeContainerList[1].UnregisterCallback<ClickEvent>(OnSecondUpgradeSelected);
        UpgradeContainerList[2].UnregisterCallback<ClickEvent>(OnThirdUpgradeSelected);
    }

    private void OnFirstUpgradeSelectes(ClickEvent evt)
    {
        upgradesRandomlySelected[0].UpgradeEvent.Raise(gameObject);
        upgradesPlayerHasSO.UpgradeList.Add(upgradesRandomlySelected[0]);
        UpgradeSelected();
        RemoveUpgradeSelectedFromList(upgradesRandomlySelected[0]);
        UnregisterAllEvents();
    }

    private void OnSecondUpgradeSelected(ClickEvent evt)
    {
        upgradesRandomlySelected[1].UpgradeEvent.Raise(gameObject);
        upgradesPlayerHasSO.UpgradeList.Add(upgradesRandomlySelected[1]);
        UpgradeSelected();
        RemoveUpgradeSelectedFromList(upgradesRandomlySelected[1]);
        UnregisterAllEvents();
    }

    private void OnThirdUpgradeSelected(ClickEvent evt)
    {
        upgradesRandomlySelected[2].UpgradeEvent.Raise(gameObject);
        upgradesPlayerHasSO.UpgradeList.Add(upgradesRandomlySelected[2]);
        UpgradeSelected();
        RemoveUpgradeSelectedFromList(upgradesRandomlySelected[2]);
        UnregisterAllEvents();
    }

    private void RemoveUpgradeSelectedFromList(Upgrade upgrade)
    {
        if(upgrade.MaxLevel > 0)
        {
            if(upgrade.MaxLevel <= upgradesPlayerHasSO.GetCurrentUpgradeLevel(upgrade)) 
            {
                foreach(Upgrade upgradeToCheck in upgradesThatCanBeSelected)
                {
                    if (upgradeToCheck.UpgradeId == upgrade.UpgradeId)
                    {
                        upgradesThatCanBeSelected.Remove(upgradeToCheck);
                        return;
                    }
                }
            }
        }
    }


    public override void OnScaledUp()
    {
        base.OnScaledUp();
        RegisterAllEvents();
    }

    public override void OnScaledDown()
    {
        base.OnScaledDown();
        SetDisplayElementNone();
    }
}
