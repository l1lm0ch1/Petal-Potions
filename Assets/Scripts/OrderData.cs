using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderItem
{
    public ScriptableObject item; // FlowerData oder PotionData
    public int amount;
}

public class OrderData
{
    public List<OrderItem> requiredItems = new List<OrderItem>();
    public int reward;
}
