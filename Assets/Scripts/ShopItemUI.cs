using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image iconImage;
    public Text nameText;
    public Text priceText;
    public Button buyButton;

    private ScriptableObject itemData;
    private int price;

    public void SetupPotion(PotionData potion)
    {
        itemData = potion;
        iconImage.sprite = potion.icon;
        nameText.text = potion.potionName;
        price = potion.basePrice;
        priceText.text = price.ToString();
    }

    public void SetupFlower(FlowerData flower)
    {
        itemData = flower;
        iconImage.sprite = flower.icon;
        nameText.text = flower.flowerName;
        price = flower.basePrice;
        priceText.text = price.ToString();
    }

    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        if (PlayerData.Instance.TryPurchase(price))
        {
            Debug.Log("Item gekauft: " + nameText.text);

            // 3D-Prefab generieren (nur als Beispiel)
            if (itemData is PotionData potion)
            {
                Instantiate(potion.prefab, Vector3.zero, Quaternion.identity);
            }
            else if (itemData is FlowerData flower)
            {
                Instantiate(flower.flowerPrefab, Vector3.zero, Quaternion.identity);
            }

            // Optional: Button deaktivieren oder UI aktualisieren
            buyButton.interactable = false;
        }
        else
        {
            Debug.Log("Nicht genug Starshards!");
        }
    }
}
