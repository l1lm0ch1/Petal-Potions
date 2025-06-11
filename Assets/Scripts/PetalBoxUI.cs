using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetalBoxUI : MonoBehaviour
{
    public PetalData petalType;
    public TMP_Text countText;
    public Image iconImage;

    void Start()
    {
        iconImage.sprite = petalType.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        int count = FlowerInventory.Instance.GetPetalCount(petalType);
        countText.text = count.ToString();
    }
}
