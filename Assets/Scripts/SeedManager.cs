using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public static SeedManager Instance;

    private FlowerData currentSeed;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetCurrentSeed(FlowerData seed)
    {
        currentSeed = seed;
    }

    public FlowerData GetCurrentSeed()
    {
        return currentSeed;
    }
}
