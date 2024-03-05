using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;
    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;
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
        if(experience > to_level_up)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        upgradePanelManager.OpenPanel();
        experience -= to_level_up;
        level += 1;
        experienceBar.SetLevelText(level);
    }
}
