using UnityEngine;

public class SeedBag : MonoBehaviour
{
    public MeshRenderer bagRenderer;
    private Material bagMaterial;

    // Start is called before the first frame update
    void Awake()
    {
        bagMaterial = bagRenderer.material;
    }
    
    public void SetBagColor(Color color)
    {
        bagMaterial.color = color;
    }
}
