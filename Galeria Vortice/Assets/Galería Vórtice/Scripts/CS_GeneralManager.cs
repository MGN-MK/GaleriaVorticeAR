using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CS_GeneralManager : MonoBehaviour
{
    public CS_Catalogue catalogue;
    public GameObject ARItem, cathalogeUI, ARUI, mainMenuUI;

    private CS_ArtInfo selectedArt;
    private void Start()
    {
        ARItem.SetActive(false);
    }
    public void StartAR()
    {
        //Set the current art from the cathaloge as the selected art
        selectedArt = catalogue.currentArt;

        //Actives and desactives the UIs
        cathalogeUI.SetActive(false);
        ARUI.SetActive(true);
        ARItem.SetActive(true);
        mainMenuUI.SetActive(false);

        //Set scale of the canvas object with the size of the selected art
        ARItem.GetComponent<MeshRenderer>().material.mainTexture = selectedArt.img.texture;
        ARItem.transform.localScale = new Vector3(selectedArt.sizes[0].y / 100, selectedArt.sizes[0].x / 100, 0.05f);
    }

    public void Cathalogue()
    {
        //Actives and desactives the UIs
        cathalogeUI.SetActive(true);
        ARUI.SetActive(false);
        ARItem.SetActive(false);
        mainMenuUI.SetActive(false);
    }

    public void MainMenu()
    {
        //Actives and desactives the UIs
        cathalogeUI.SetActive(false);
        ARUI.SetActive(false);
        ARItem.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
