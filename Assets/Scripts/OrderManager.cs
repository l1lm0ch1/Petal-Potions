using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("UI")]
    public OrderUI orderUI;

    [Header("Order Settings")]
    public int minItemsPerOrder = 1;
    public int maxItemsPerOrder = 3;

    public int minAmountPerItem = 1;
    public int maxAmountPerItem = 3;

    public int minReward = 100;
    public int maxReward = 350;

    [Header("Starting Potion")]
    public PotionData firstPotion;

    //[Header("Available Items")]
    //public List<PotionData> allPotions;

    private List<OrderData> currentOrders = new();

    public List<OrderData> GetCurrentOrders() => currentOrders;

    private bool firstOrderfinished = false;

    public void GenerateOrders(int numberOfOrders)
    {
        currentOrders.Clear();

        List<PotionData> craftedPotions = PotionTracker.Instance.GetAllCraftedPotions();

        // Speziell: Erste Order ist immer der Balance Potion
        if (firstPotion != null && craftedPotions.Contains(firstPotion) && !firstOrderfinished)
        {
            OrderData firstOrder = new OrderData();
            firstOrder.requiredItems.Add(new OrderItem
            {
                item = firstPotion,
                amount = Random.Range(minAmountPerItem, maxAmountPerItem + 1)
            });
            firstOrder.reward = Random.Range(minReward, maxReward + 1);
            currentOrders.Add(firstOrder);
            numberOfOrders--; // eine weniger generieren
            firstOrderfinished = true;
        }

        // Restliche Orders generieren
        for (int i = 0; i < numberOfOrders; i++)
        {
            OrderData order = new OrderData();

            // Nur Potions verwenden, die man schon gecraftet hat
            if (craftedPotions.Count == 0) break;

            int itemCount = Random.Range(minItemsPerOrder, Mathf.Min(craftedPotions.Count, maxItemsPerOrder + 1));

            List<ScriptableObject> pool = new();
            pool.AddRange(craftedPotions);

            for (int j = 0; j < itemCount; j++)
            {
                if (pool.Count == 0) break;

                int randIndex = Random.Range(0, pool.Count);
                ScriptableObject randomItem = pool[randIndex];
                pool.RemoveAt(randIndex);

                int randomAmount = Random.Range(minAmountPerItem, maxAmountPerItem + 1);

                order.requiredItems.Add(new OrderItem
                {
                    item = randomItem,
                    amount = randomAmount
                });
            }

            order.reward = Random.Range(minReward, maxReward + 1);
            currentOrders.Add(order);
        }

        Debug.Log("Orders generiert: " + currentOrders.Count);

        if (orderUI != null && currentOrders.Count > 0)
        {
            orderUI.Setup(currentOrders[0]);
        }
    }
}
