using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderItemEntry : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text amountText;

    public void Setup(Sprite icon, string name, int amount)
    {
        iconImage.sprite = icon;
        nameText.text = name;
        amountText.text = "x" + amount.ToString();
    }
}
