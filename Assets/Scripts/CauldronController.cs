using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CauldronController : MonoBehaviour
{
    [Header("Particle Systems")]
    public ParticleSystem failedPotion;
    public ParticleSystem createdPotion;

    [Header("SFX für Potioncrafting")]
    public AudioSource potionSuccess;
    public AudioSource potionFailure;

    [Header("Potion Spawnpunkt")]
    public Transform spawnPoint;

    [Header("Liste aller Potion Kombinationen")]
    public List<PotionCombination> potionCombinations;
    public List<string> allPetalIDs;

    private List<string> currentPetals = new();

    private void OnTriggerEnter(Collider other)
    {
        // Holt das PetalDataHolder Script vom Objekt, das den Collider ausgelöst hat
        PetalDataHolder holder = other.GetComponent<PetalDataHolder>();
        if (holder == null || holder.petalData == null || currentPetals.Count >= 2)
            return;

        // Holt die ID aus dem ScriptableObject
        currentPetals.Add(holder.petalData.petalID);
        Destroy(other.gameObject);
    }

    public void TryCraftPotion()
    {
        currentPetals.Sort(); // Reihenfolge egal
        string key = string.Join("+", currentPetals);

        foreach (var combo in potionCombinations)
        {
            if (combo.requiresAllPetals)
            {
                // Check: Ist die aktuelle Liste identisch mit ALLEN verfügbaren Petals?
                List<string> sortedAll = new List<string>(allPetalIDs);
                sortedAll.Sort();

                if (currentPetals.Count == sortedAll.Count && !currentPetals.Except(sortedAll).Any())
                {
                    SpawnPotion(combo.potionPrefab);
                    return;
                }
            }
            else if (combo.MatchKey == key)
            {
                SpawnPotion(combo.potionPrefab);
                return;
            }
        }

        // Keine passende Kombination gefunden
        Debug.Log("Ungültige Kombination: " + key);
        potionFailure.Play();
        currentPetals.Clear();
        failedPotion.Play();
    }

    private void SpawnPotion(GameObject prefab)
    {
        createdPotion.Play();
        potionSuccess.Play();

        GameObject craftedPotion = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        currentPetals.Clear();

        PotionDataHolder instance = craftedPotion.GetComponent<PotionDataHolder>();
        if (instance != null && instance.potionData != null)
        {
            PotionTracker.Instance.RegisterCraftedPotion(instance.potionData);
        }
        else
        {
            Debug.LogWarning("PotionData nicht gesetzt am Prefab!");
        }
    }
}
