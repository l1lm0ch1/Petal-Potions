using UnityEngine;
using System.Collections.Generic;

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
        // Holt das PetalDataHolder Script vom Objekt, das den Collider ausgelöst hat
        PetalDataHolder holder = other.GetComponent<PetalDataHolder>();
        if (holder == null || holder.petalData == null || currentPetals.Count >= 2)
            return;

        // Holt die ID aus dem ScriptableObject
        currentPetals.Add(holder.petalData.petalID);
        Destroy(other.gameObject);
        /*
        if (currentPetals.Count == 2)
            TryCraftPotion();*/
    }

    public void TryCraftPotion()
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
