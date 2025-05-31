using System.Collections.Generic;
using UnityEngine;

public class SellBoxTrigger : MonoBehaviour
{
    public ShopManager shopManager;

    private List<GameObject> insideObjects = new();

    void OnTriggerEnter(Collider other)
    {
        if (!insideObjects.Contains(other.gameObject))
            insideObjects.Add(other.gameObject);

        CheckOrders();
    }

    void OnTriggerExit(Collider other)
    {
        if (insideObjects.Contains(other.gameObject))
            insideObjects.Remove(other.gameObject);
    }

    void CheckOrders()
    {
        foreach (OrderData order in shopManager.GetCurrentOrders())
        {
            bool orderFulfilled = true;

            // Prüfen, ob alle Items da sind
            foreach (OrderItem item in order.requiredItems)
            {
                int countInBox = insideObjects.FindAll(obj =>
                {
                    ItemHolder holder = obj.GetComponent<ItemHolder>();
                    return holder != null && holder.data == item.item;
                }).Count;

                if (countInBox < item.amount)
                {
                    orderFulfilled = false;
                    break;
                }
            }

            if (orderFulfilled)
            {
                Debug.Log("Auftrag erfüllt! Belohnung: " + order.reward);

                // Belohnung geben
                PlayerData.Instance.AddStarshards(order.reward);

                // Entferne nur die Objekte des erfüllten Auftrags
                foreach (OrderItem item in order.requiredItems)
                {
                    int toRemove = item.amount;
                    for (int i = insideObjects.Count - 1; i >= 0 && toRemove > 0; i--)
                    {
                        ItemHolder holder = insideObjects[i].GetComponent<ItemHolder>();
                        if (holder != null && holder.data == item.item)
                        {
                            Destroy(insideObjects[i]);
                            insideObjects.RemoveAt(i);
                            toRemove--;
                        }
                    }
                }

                // Neue Orders generieren
                shopManager.GenerateOrders();

                break; // Nur 1 Auftrag gleichzeitig erfüllen
            }
        }
    }

}
