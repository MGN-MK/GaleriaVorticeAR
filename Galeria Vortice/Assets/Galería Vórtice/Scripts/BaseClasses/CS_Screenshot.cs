using UnityEngine;

[System.Serializable]
public class CS_Screenshot : MonoBehaviour
{
    public Sprite image;
    public int id;

    public void Set(Sprite _img, int _id)
    {
        image = _img;
        id = _id;
    }

    public void OnClick()
    {
        CS_AudioManager.instance.PlaySFX("UI");
        FindObjectOfType<CS_GalleryManager>().SelectScreenshot(this);
    }
}
