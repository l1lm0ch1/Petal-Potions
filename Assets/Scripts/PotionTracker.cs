using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionTracker : MonoBehaviour
{
    public static PotionTracker Instance;

    private HashSet<PotionData> craftedPotions = new();

    public event Action<PotionData> OnPotionCrafted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterCraftedPotion(PotionData potion)
    {
        if(craftedPotions.Add(potion))
        {
            OnPotionCrafted?.Invoke(potion);
        }
    }

    public bool HasCrafted(PotionData potion)
    {
        return craftedPotions.Contains(potion);
    }

    public List<PotionData> GetAllCraftedPotions()
    {
        return new List<PotionData>(craftedPotions);
    }
}
