using UnityEngine;

public class SeedPlacer : MonoBehaviour
{
    private bool isOccupied = false;
    public ParticleSystem ParticleSystem;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Seed found");
        if (isOccupied) return;

        Seed seed = other.GetComponent<Seed>();
        if(seed != null && seed.flowerData != null )
        {
            Debug.Log("Flower Planted");
            SpawnFlower(seed.flowerData);
            Destroy(other.gameObject);
            isOccupied = true;
        }
    }

    private void SpawnFlower(FlowerData data)
    {
        GameObject flowerGO = Instantiate(data.flowerPrefab, transform.position, data.flowerPrefab.transform.rotation);
        FlowerGrowth growth = flowerGO.GetComponent<FlowerGrowth>();
        if(growth != null)
        {
            growth.flowerData = data;
            growth.enabled = true;
        }

        Debug.Log($"Blume '{data.flowerName}' wurde im Slot gepflanzt");
        ParticleSystem.Play();
    }

    public void ResetSlot()
    {
        isOccupied = false;
    }
}
