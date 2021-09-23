using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{

    [SerializeField] private Transform tutorialPanel;
    [SerializeField] private Transform campaignPanel;
    [SerializeField] private Transform garagePanel;
    [SerializeField] private Transform settingsPanel;

    [SerializeField] private bool DescriptionOn = false;

    private Transform tutorialTitle;
    private Transform campaignTitle;
    private Transform garageTitle;
    private Transform settingsTitle;

    private Transform tutorialDescription;
    private Transform campaignDescription;
    private Transform garageDescription;
    private Transform settingsDescription;


    private void Start()
    {
        DescriptionOn = false;

        tutorialTitle = tutorialPanel.GetChild(0);
        tutorialDescription = tutorialPanel.GetChild(1);

        campaignTitle = campaignPanel.GetChild(0);
        campaignDescription = campaignPanel.GetChild(1);
        
        garageTitle = garagePanel.GetChild(0);
        garageDescription = garagePanel.GetChild(1);
        
        settingsTitle = settingsPanel.GetChild(0);
        settingsDescription = settingsPanel.GetChild(1);
    }

    private void Update()
    {
        if(DescriptionOn)
        {
            tutorialDescription.gameObject.SetActive(true);
            campaignDescription.gameObject.SetActive(true);
            garageDescription.gameObject.SetActive(true);
            //settingsDescription.gameObject.SetActive(true);

            tutorialTitle.gameObject.SetActive(false);
            campaignTitle.gameObject.SetActive(false);
            garageTitle.gameObject.SetActive(false);
            //settingsTitle.gameObject.SetActive(false);
        }
        else
        {
            tutorialDescription.gameObject.SetActive(false);
            campaignDescription.gameObject.SetActive(false);
            garageDescription.gameObject.SetActive(false);
            //settingsDescription.gameObject.SetActive(false);

            tutorialTitle.gameObject.SetActive(true);
            campaignTitle.gameObject.SetActive(true);
            garageTitle.gameObject.SetActive(true);
            //settingsTitle.gameObject.SetActive(true);
        }
    }


    public void SwitchDescription()
    {
        DescriptionOn = !DescriptionOn;
    }

}
