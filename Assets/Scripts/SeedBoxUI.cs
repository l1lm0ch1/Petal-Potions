using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedBoxUI : MonoBehaviour
{
    public SeedData seedType;
    public TMP_Text countText;
    public Image iconImage;

    void Start()
    {
        iconImage.sprite = seedType.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        int count = SeedInventory.Instance.GetSeedCount(seedType);
        countText.text = count.ToString();
    }
}
