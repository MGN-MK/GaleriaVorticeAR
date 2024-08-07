using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CS_CatalogueManager : MonoBehaviour
{
    public CS_ArtInfo[] artCatalogue;

    [Header("UI References")]
    public TextMeshProUGUI titletxt, yeartxt, artisttxt, techniquetxt, sizetxt;
    public GameObject image;

    private int index = 0;
    public CS_ArtInfo currentArt;

    private float x, y;

    private void Start()
    {
        ChangeArt(0);
    }

    //Change showed art, value should be -1/+1
    public void ChangeArt(int value)
    {
        CS_AudioManager.instance.PlaySFX("UI");

        //Adds the value(-1 or 1) to get the previus or next item
        index += value;

        //Sets the index in case of get outside the array
        if(index < 0) { index = artCatalogue.Length -1; }
        if(index > artCatalogue.Length -1) { index = 0; }

        currentArt = artCatalogue[index];

        //Update the text data to show
        titletxt.text = currentArt.title;
        yeartxt.text = currentArt.year.ToString();
        artisttxt.text = currentArt.artist.ToString().Replace("_", " ");
        techniquetxt.gameObject.GetComponent<LocalizedText>().LocalizationKey = currentArt.technique.ToString();

        if (CS_MultilanguajeManager.instance.currentLanguaje == "Spanish")
        {
            sizetxt.text = currentArt.sizes[0].x + " cm x " + currentArt.sizes[0].y + " cm";
        } else if (CS_MultilanguajeManager.instance.currentLanguaje == "English")
        {
            sizetxt.text = (currentArt.sizes[0].x / 2.54f).ToString("F2") + " in x " + (currentArt.sizes[0].y / 2.54f).ToString("F2") + " in";
        }
        else
        {
            sizetxt.text = currentArt.sizes[0].x + " cm x " + currentArt.sizes[0].y + " cm";
        }

        //Update IMG sprite
        image.GetComponent<Image>().sprite = currentArt.img;

        //Calculate ratio aspect
        if (currentArt.sizes[0].x == currentArt.sizes[0].y)
        {
            x = 700;
            y = 700;
        }
        else if (currentArt.sizes[0].x > currentArt.sizes[0].y)
        {
            x = 700;
            y = x * currentArt.sizes[0].y / currentArt.sizes[0].x;
        }
        else
        {
            y = 700;
            x = y * currentArt.sizes[0].x / currentArt.sizes[0].y;
        }

        image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, y);
        image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x);
    }
}
