using UnityEngine;
using TMPro;

public class OrderUI : MonoBehaviour
{
    public TMP_Text titleText;
    public Transform itemsContainer; // Hier werden OrderItemEntries reingepackt
    public GameObject itemEntryPrefab; // Prefab von OrderItemEntry
    public TMP_Text rewardText;

    public void Setup(OrderData order)
    {
        // Titel (optional)
        titleText.text = "Current Order";

        // Alte Entries löschen
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Neue Entries erzeugen
        foreach (OrderItem item in order.requiredItems)
        {
            GameObject entryGO = Instantiate(itemEntryPrefab, itemsContainer);
            OrderItemEntry entry = entryGO.GetComponent<OrderItemEntry>();

            // Setup
            if (item.item is PotionData potion)
            {
                entry.Setup(potion.icon, potion.potionName, item.amount);
            }
            else if (item.item is PetalData petal)
            {
                entry.Setup(petal.icon, petal.petalName, item.amount);
            }
        }

        // Belohnungstext setzen
        rewardText.text = $"Reward: {order.reward} Starshards";
    }
}
