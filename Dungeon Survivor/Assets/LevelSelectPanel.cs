using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : MonoBehaviour
{
    [SerializeField] List<GameObject> stageSelectButton;
    [SerializeField] DataContainer dataContainer;
    void UpdateButtons()
    {
        stageSelectButton[0].SetActive(true);
        for (int i = 1; i < stageSelectButton.Count; i++)
        {
            if (dataContainer.stageCompletion[i - 1] == true)
            {

                stageSelectButton[i].SetActive(true);
            }
            else
            {
                stageSelectButton[i].SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        UpdateButtons();
    }
}
