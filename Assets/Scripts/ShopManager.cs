using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public OrderManager orderManager;
    public int ordersToGenerate = 3;

    void Start()
    {
        GenerateOrders();
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
