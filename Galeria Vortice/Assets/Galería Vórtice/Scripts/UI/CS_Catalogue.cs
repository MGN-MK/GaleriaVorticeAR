using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CS_Catalogue : MonoBehaviour
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
        index += value;

        if(index < 0) { index = artCatalogue.Length -1; }
        if(index > artCatalogue.Length -1) { index = 0; }

        currentArt = artCatalogue[index];

        //Update the text data to show
        titletxt.text = currentArt.title;
        yeartxt.text = currentArt.year.ToString();
        artisttxt.text = currentArt.artist.ToString().Replace("_", " ");
        techniquetxt.text = currentArt.technique.ToString().Replace("_", " ");
        sizetxt.text = currentArt.sizes[0].x + " cm x " + currentArt.sizes[0].y + " cm";

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
