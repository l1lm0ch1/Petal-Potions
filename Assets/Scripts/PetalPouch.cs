using UnityEngine;

public class PetalPouch : MonoBehaviour
{
    public MeshRenderer pouchRenderer;
    private PetalData currentPetal;

    public void UpdatePouchVisual(PetalData newPetal)
    {
        currentPetal = newPetal;
        pouchRenderer.material.color = newPetal.icon != null ? AverageColorFromTexture(newPetal.icon.texture) : Color.white;
    }

    Color AverageColorFromTexture(Texture2D tex)
    {
        Color[] pixels = tex.GetPixels();
        Color avg = new Color(0, 0, 0);
        foreach (var p in pixels) avg += p;
        return avg / pixels.Length;
    }

    public PetalData GetSelectedPetal() => currentPetal;
}
