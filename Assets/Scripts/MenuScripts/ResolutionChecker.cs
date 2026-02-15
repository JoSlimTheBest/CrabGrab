using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionChecker : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            ChangeQuality(PlayerPrefs.GetInt("Quality"));
        }
        else
        {
            ChangeQuality(5);
        }

        if (PlayerPrefs.HasKey("Reolut"))
        {
            ChangeResolution(PlayerPrefs.GetInt("Reolut"));
        }
    }

    public void ChangeQuality(int option)
    {
        QualitySettings.SetQualityLevel(option);

        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
    }


    public void ChangeResolution(int option)
    {
        if(option == 0)
        {
            Screen.SetResolution(1920, 1080,true);
        }

        if (option == 1)
        {
            Screen.SetResolution(3840,2160, true);
        }

        if (option == 2)
        {
            Screen.SetResolution(2560,1440, true);
        }

        if (option == 3)
        {
            Screen.SetResolution(1366,768, true);
        }


        if (option == 4)
        {
            Screen.SetResolution(1920, 1080, false);
        }

        if (option == 5)
        {
            Screen.SetResolution(3840, 2160, false);
        }

        if (option == 6)
        {
            Screen.SetResolution(2560, 1440, false);
        }

        if (option == 7)
        {
            Screen.SetResolution(1366, 768, false);
        }

        PlayerPrefs.SetInt("Reolut", option);

    }


}
