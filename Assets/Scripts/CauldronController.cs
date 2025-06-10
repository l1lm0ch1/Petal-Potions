using UnityEngine;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CauldronController : MonoBehaviour
{
    public ParticleSystem failedPotion;
    public ParticleSystem createdPotion;
    public Transform spawnPoint;
    public List<PotionCombination> potionCombinations; // im Inspector pflegbar
    private List<string> currentPetals = new();
    public AudioSource potionSuccess;
    public AudioSource potionFailure;

    private void OnTriggerEnter(Collider other)
    {
        PetalData petal = other.GetComponent<PetalData>();
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
                createdPotion.Play();
                potionSuccess.Play();
                Instantiate(combo.potionPrefab, spawnPoint.position, Quaternion.identity);
                currentPetals.Clear();
                return;
            }
        }

        Debug.Log("Ungültige Kombination: " + key);
        potionFailure.Play();
        currentPetals.Clear();
        failedPotion.Play();
    }
}
