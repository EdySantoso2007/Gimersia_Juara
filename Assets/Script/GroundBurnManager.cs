using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundBurnManager : MonoBehaviour
{
    public static GroundBurnManager Instance;

    [Header("Tilemap References")]
    public Tilemap groundTilemap;
    public TileBase grassTile;      // Tile rumput hijau normal
    public TileBase burnedTile;     // Tile tanah cokelat terbakar

    void Awake()
    {
        Instance = this;
    }

    // Dipanggil saat bola api mengenai tanah
    public void BurnGroundAt(Vector3 worldPosition)
    {
        if (groundTilemap == null || burnedTile == null) return;

        Vector3Int cellPos = groundTilemap.WorldToCell(worldPosition);
        TileBase currentTile = groundTilemap.GetTile(cellPos);

        // Hanya ubah jika posisi tersebut adalah tile rumput hijau
        if (currentTile == grassTile)
        {
            groundTilemap.SetTile(cellPos, burnedTile);
        }
    }

    // Opsional: Dipanggil peluru air jika ingin tanah cokelat bisa dipulihkan jadi hijau lagi
    public void RestoreGroundAt(Vector3 worldPosition)
    {
        if (groundTilemap == null || grassTile == null) return;

        Vector3Int cellPos = groundTilemap.WorldToCell(worldPosition);
        if (groundTilemap.GetTile(cellPos) == burnedTile)
        {
            groundTilemap.SetTile(cellPos, grassTile);
        }
    }
}