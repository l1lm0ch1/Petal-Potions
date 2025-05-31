using UnityEngine;

public class PlantBox : MonoBehaviour
{
    public Transform plantSpawnPoint;

    void OnTriggerEnter(Collider other)
    {
        Seed seed = other.GetComponent<Seed>();
        if (seed != null)
        {
            PlantFlower(seed.flowerData);
            Destroy(other.gameObject); // Samen „verschwindet“
        }
    }

    void PlantFlower(FlowerData flowerData)
    {
        GameObject flowerPrefab = flowerData.flowerPrefab; // im FlowerData
        GameObject flower = Instantiate(flowerPrefab, plantSpawnPoint.position, Quaternion.identity);

        Debug.Log("Blume gepflanzt: " + flowerData.flowerName);
    }
}
