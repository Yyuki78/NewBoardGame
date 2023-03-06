using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] GameObject RulePanel;

    void Start()
    {
        RulePanel.SetActive(false);
    }

    public void OnClick(int num)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("SampleScene");
                break;
            case 1:
                RulePanel.SetActive(true);
                break;
            case 2:
                RulePanel.SetActive(false);
                break;
        }
    }
}
