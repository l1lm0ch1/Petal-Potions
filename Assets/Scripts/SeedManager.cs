using UnityEngine;

public class SeedManager : MonoBehaviour
{
    public static SeedManager Instance;
    private SeedData currentSeed;

    public SeedBag seedBag;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetCurrentSeed(SeedData seed)
    {
        currentSeed = seed;
        Debug.Log("Aktiver Samen gesetzt: " +  currentSeed);

        if (seedBag != null)
        {
            seedBag.SetBagColor(currentSeed.bagColor);
        }
    }

    public SeedData GetCurrentSeed()
    {
        return currentSeed;
    }
}
