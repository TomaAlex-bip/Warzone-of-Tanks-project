using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainerMovement : MonoBehaviour
{
    private Vector3 desiredMenuPosition;

    private RectTransform menuContainter;


    private void Start()
    {
        menuContainter = gameObject.GetComponent<RectTransform>();
        NavigateTo(Menu.MainMenuPanel);
    }

    private void Update()
    {
        menuContainter.anchoredPosition3D = Vector3.Lerp(menuContainter.anchoredPosition3D, desiredMenuPosition, 10 * Time.deltaTime);
    }


    public void NavigateTo(Menu menu)
    {
        switch(menu)
        {
            default:

            case Menu.MainMenuPanel:
                desiredMenuPosition = Vector3.zero;
                break;

            case Menu.TutorialPanel:
                desiredMenuPosition = new Vector3(1920, 0, 0);
                break;

            case Menu.GaragePanel:
                desiredMenuPosition = new Vector3(-1920, 0, 0);
                break;

            case Menu.CampaignPanel:
                desiredMenuPosition = new Vector3(0, 1080, 0);
                break;
        }
    }

    public void NavigateToIndex(int menuIndex)
    {
        switch (menuIndex)
        {
            default:

                // main menu
            case 1:
                desiredMenuPosition = Vector3.zero;
                break;

                // tutorial
            case 2:
                desiredMenuPosition = new Vector3(1920, 0, 0);
                break;

                //garage
            case 3:
                desiredMenuPosition = new Vector3(-1920, 0, 0);
                break;

                //campaign
            case 4:
                desiredMenuPosition = new Vector3(0, 1080, 0);
                break;
        }
    }

}



