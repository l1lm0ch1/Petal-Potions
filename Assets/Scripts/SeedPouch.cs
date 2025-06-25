using UnityEngine;

public class SeedPouch : MonoBehaviour
{
    public MeshRenderer pouchRenderer;
    private SeedData currentSeed;

    public void UpdatePouchVisual(SeedData newSeed)
    {
        currentSeed = newSeed;
        pouchRenderer.material.color = newSeed.icon != null ? AverageColorFromSprite(newSeed.icon) : Color.white;
    }

    Color AverageColorFromSprite(Sprite sprite)
    {
        // Hole die Textur, auf der das Sprite basiert
        Texture2D tex = sprite.texture;

        // Bestimme den Bereich des Sprites auf der Textur (in Pixeln)
        Rect rect = sprite.textureRect;

        // Hole alle Pixel im Sprite-Bereich
        Color[] pixels = tex.GetPixels(
            (int)rect.x,
            (int)rect.y,
            (int)rect.width,
            (int)rect.height);

        Color avg = new Color(0, 0, 0, 0);
        foreach (var p in pixels)
            avg += p;

        return avg / pixels.Length;
    }


    public SeedData GetSelectedSeed() => currentSeed;
}
