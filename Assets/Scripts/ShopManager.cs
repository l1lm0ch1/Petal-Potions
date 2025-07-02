using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public OrderManager orderManager;
    public int ordersToGenerate = 3;
    private bool firstOrderGenerated = false;

    void Start()
    {
        PotionTracker.Instance.OnPotionCrafted += HandlePotionCrafted;
    }

    private void HandlePotionCrafted(PotionData potion)
    {
        if (!firstOrderGenerated)
        {
            GenerateOrders();
            firstOrderGenerated = true;
        }
    }

    public void GenerateOrders()
    {
        orderManager.GenerateOrders(ordersToGenerate);
    }

    public List<OrderData> GetCurrentOrders()
    {
        return orderManager.GetCurrentOrders();
    }
}
