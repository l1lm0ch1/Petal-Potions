using System.Collections.Generic;
using UnityEngine;

public class SellBoxTrigger : MonoBehaviour
{
    [Header("Shopmanager Reference")]
    public ShopManager shopManager;

    [Header("Order complete Feedback")]
    public ParticleSystem orderComplete;
    public AudioSource orderSuccess;

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
                    PotionDataHolder holder = obj.GetComponent<PotionDataHolder>();
                    return holder != null && holder.potionData == item.item;
                }).Count;

                if (countInBox < item.amount)
                {
                    orderFulfilled = false;
                    break;
                }
            }

            if (orderFulfilled)
            {
                orderComplete.Play();
                orderSuccess.Play();

                Debug.Log("Auftrag erfüllt! Belohnung: " + order.reward);

                // Belohnung geben
                PlayerData.Instance.AddStarshards(order.reward);

                // Entferne nur die Objekte des erfüllten Auftrags
                foreach (OrderItem item in order.requiredItems)
                {
                    int toRemove = item.amount;
                    for (int i = insideObjects.Count - 1; i >= 0 && toRemove > 0; i--)
                    {
                        PotionDataHolder holder = insideObjects[i].GetComponent<PotionDataHolder>();
                        if (holder != null && holder.potionData == item.item)
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
