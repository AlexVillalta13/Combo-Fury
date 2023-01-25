using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChooseUpgradeUI : UIComponent
{
    [SerializeField] UpgradeInLevelSO upgradeListSO;
    [SerializeField] GameEvent continueWalkingEvent;

    Button continueButton;
    List<VisualElement> UpgradeContainerList = new List<VisualElement>();
    List<UpgradeInLevelSO.Upgrade> upgradesRandomlySelected = new List<UpgradeInLevelSO.Upgrade>();

    const string upgradeContainerClass = "upgradeContainer";
    const string upgradeNameClass = "nameUpgrade";
    const string upgradeDescriptionClass = "descriptionUpgrade";

    public override void SetElementsReferences()
    {
        base.SetElementsReferences();

        continueButton = m_UIElement.Query<Button>();
        UpgradeContainerList = m_UIElement.Query<VisualElement>(className: upgradeContainerClass).ToList();
    }

    private void OnEnable()
    {
        continueButton.clicked += ContinueButtonPressed;
    }

    private void OnDisable()
    {
        continueButton.clicked -= ContinueButtonPressed;
    }

    public void ContinueButtonPressed()
    {
        continueWalkingEvent.Raise();
        HideGameplayElement();
    }

    public void SelectRandomUpgrades()
    {
        upgradesRandomlySelected.Clear();
        int z = 0;
        while(upgradesRandomlySelected.Count < 3)
        {
            z++;
            Debug.Log("Enter loop: " + z);
            int i = Random.Range(0, upgradeListSO.UpgradeList.Count);
            UpgradeInLevelSO.Upgrade upgrade = upgradeListSO.UpgradeList[i];
            if (upgradesRandomlySelected.Count == 0)
            {
                upgradesRandomlySelected.Add(upgrade);
                Debug.Log("First loop: " + z);
            }
            else
            {
                foreach (UpgradeInLevelSO.Upgrade upgradeToCheck in upgradesRandomlySelected)
                {
                    if (upgradeToCheck.UpgradeName == upgrade.UpgradeName)
                    {
                        Debug.Log("Continue. loop: " + z);
                        goto OuterLoop;
                    }
                }

                if (upgrade.CanRepeat == false)
                {
                    // Check unlocked upgrades to see if can appear again
                }
                Debug.Log("No can repeat or no repeated upgrades. loop: " + z);
                upgradesRandomlySelected.Add(upgrade);
            }
            OuterLoop:
            Debug.Log("Outerloop: " + z);
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
}
