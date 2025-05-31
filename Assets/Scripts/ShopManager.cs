using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Item Pools")]
    public List<PotionData> allPotions;
    public List<FlowerData> allFlowers;

    [Header("Shop Settings")]
    public int itemsToSell = 5;

    [Header("UI")]
    public Transform shopContent;
    public GameObject shopItemPrefab;

    private List<ScriptableObject> currentShopItems = new List<ScriptableObject>();

    void Start()
    {
        GenerateShopItems();
    }

    public void GenerateShopItems()
    {
        // Clear old UI
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }
        currentShopItems.Clear();

        // Combine Flowers + Potions
        List<ScriptableObject> pool = new List<ScriptableObject>();
        pool.AddRange(allPotions);
        pool.AddRange(allFlowers);

        // Zufällige Auswahl
        for (int i = 0; i < itemsToSell; i++)
        {
            if (pool.Count == 0) break;

            int randIndex = Random.Range(0, pool.Count);
            ScriptableObject randomItem = pool[randIndex];
            pool.RemoveAt(randIndex);

            // Neues UI-Element
            GameObject itemGO = Instantiate(shopItemPrefab, shopContent);
            ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();

            if (randomItem is PotionData potion)
            {
                itemUI.SetupPotion(potion);
            }
            else if (randomItem is FlowerData flower)
            {
                itemUI.SetupFlower(flower);
            }

            currentShopItems.Add(randomItem);
        }
    }
}
