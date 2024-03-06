using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;
    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;
    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrade;
    [SerializeField]  List<UpgradeData> acquiredUpgrades;
    private int to_level_up
    {
        get
        {
            return level * 1000;
        }
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, to_level_up);
        experienceBar.SetLevelText(level);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        checkLevelUp();
        experienceBar.UpdateExperienceSlider(experience, to_level_up);
    }

    public void checkLevelUp()
    {
        if (experience > to_level_up)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(selectedUpgrade == null) { selectedUpgrade = new List<UpgradeData>(); }
        selectedUpgrade.Clear();
        selectedUpgrade.AddRange(GetUpgrades(3));


        upgradePanelManager.OpenPanel(selectedUpgrade);
        experience -= to_level_up;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if(count >upgrades.Count)
        {
            count = upgrades.Count;
        }
        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrade[selectedUpgradeId];
        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }
}
