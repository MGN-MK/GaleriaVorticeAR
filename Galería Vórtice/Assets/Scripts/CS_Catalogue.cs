using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CS_Catalogue : MonoBehaviour
{
    public CS_ArtInfo[] artCatalogue;

    [Header("UI References")]
    public TextMeshProUGUI titletxt;
    public TextMeshProUGUI artisttxt;
    public TextMeshProUGUI techniquetxt;
    public TextMeshProUGUI sizetxt;
    public Image image;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
