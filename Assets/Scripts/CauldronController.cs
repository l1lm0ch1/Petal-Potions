using UnityEngine;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CauldronController : MonoBehaviour
{
    public Transform spawnPoint;
    public List<PotionCombination> potionCombinations; // im Inspector pflegbar
    private List<string> currentPetals = new();

    private void OnTriggerEnter(Collider other)
    {
        Petal petal = other.GetComponent<Petal>();
        if (petal == null || currentPetals.Count >= 2) return;

        currentPetals.Add(petal.petalID);
        Destroy(other.gameObject);

        if (currentPetals.Count == 2)
            TryCraftPotion();
    }

    void TryCraftPotion()
    {
        currentPetals.Sort(); // alphabetisch, um Reihenfolge zu ignorieren
        string key = string.Join("+", currentPetals);

        foreach (var combo in potionCombinations)
        {
            if (combo.MatchKey == key)
            {
                Instantiate(combo.potionPrefab, spawnPoint.position, Quaternion.identity);
                currentPetals.Clear();
                return;
            }
        }

        Debug.Log("Ungültige Kombination: " + key);
        currentPetals.Clear();
    }
}
