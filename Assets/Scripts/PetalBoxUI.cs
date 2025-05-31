using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetalBoxUI : MonoBehaviour
{
    public FlowerData flowerType;
    public TMP_Text countText;
    public Image iconImage;

    void Start()
    {
        iconImage.sprite = flowerType.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        int count = FlowerInventory.Instance.GetPetalCount(flowerType);
        countText.text = count.ToString();
    }
}
