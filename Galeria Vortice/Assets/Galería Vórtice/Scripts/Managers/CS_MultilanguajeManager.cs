using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CS_MultilanguajeManager : MonoBehaviour
{
    public static CS_MultilanguajeManager instance;

    public string currentLanguaje;
    public string[] languajes;
    private int id = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        LocalizationManager.Read();

        switch(Application.systemLanguage)
        {
            case SystemLanguage.Spanish:
                LocalizationManager.Language = "Spanish";
                id = 0;
                break;

            case SystemLanguage.English:
                LocalizationManager.Language = "English";
                id = 1;
                break;
        }
    }

    public void ChangeLanguaje()
    {
        id++;
        if(id >= languajes.Length)
        {
            id=0;
        }

        LocalizationManager.Language = languajes[id];
        currentLanguaje = languajes[id];

        CS_GeneralManager.instance.catalogue.ChangeArt(0);
    }
}
