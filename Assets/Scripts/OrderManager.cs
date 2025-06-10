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
    public int maxAmountPerItem = 5;

    public int minReward = 10;
    public int maxReward = 150;

    [Header("Available Items")]
    public List<FlowerData> allFlowers;
    public List<PotionData> allPotions;

    private List<OrderData> currentOrders = new();

    public List<OrderData> GetCurrentOrders() => currentOrders;

    private void Awake()
    {
        GenerateOrders(10);
    }

    public void GenerateOrders(int numberOfOrders)
    {
        currentOrders.Clear();

        for (int i = 0; i < numberOfOrders; i++)
        {
            OrderData order = new OrderData();
            int itemCount = Random.Range(minItemsPerOrder, maxItemsPerOrder + 1);

            // Mögliche Items Pool
            List<ScriptableObject> pool = new();
            pool.AddRange(allFlowers);
            pool.AddRange(allPotions);

            // Random items für Order
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

        if(orderUI != null && currentOrders.Count > 0)
        {
            orderUI.Setup(currentOrders[0]);
        }
    }
}
