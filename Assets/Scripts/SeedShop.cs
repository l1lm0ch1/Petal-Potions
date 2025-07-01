using UnityEngine;

public class SeedShop : MonoBehaviour
{
    [Header("Verfügbare Seeds")]
    public SeedData[] seedOptions;

    public void BuySeedByIndex(int index)
    {
        if (index < 0 || index >= seedOptions.Length)
        {
            Debug.LogWarning("Ungültiger Seed-Index!");
            return;
        }

        SeedData seedToBuy = seedOptions[index];
        Debug.Log(index);
        Debug.Log(seedToBuy);
        int cost = seedToBuy.Price;

        if (PlayerData.Instance.GetStarshards() >= cost)
        {
            SeedInventory.Instance.AddSeed(seedToBuy, 1);

            PlayerData.Instance.AddStarshards(-cost);

            //Debug.Log($"Seed gekauft: {seedToBuy.seedName} für {cost} Starshards");
        }
        else
        {
            //Debug.Log($"Nicht genug Starshards für {seedToBuy.seedName} (benötigt: {cost})");
        }
    }
}
