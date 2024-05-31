using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CS_ArtInfo", order = 1)]
public class CS_ArtInfo : ScriptableObject
{
    public string title;
    public float year;
    public Artist artist;
    public Technique technique;
    public Vector2[] sizes;
    public Sprite img;
}
