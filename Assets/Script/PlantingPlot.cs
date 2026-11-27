using UnityEngine;

public class PlantingPlot : MonoBehaviour
{
    [Header("Plot Settings")]
    public GameObject treePrefab;
    public bool isPlanted = false;

    private GameObject currentTree;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlanted || treePrefab == null) return;

        if (other.CompareTag("Player"))
        {
            SeedSpreader spreader = other.GetComponent<SeedSpreader>();
            if (spreader != null && spreader.canPlant)
            {
                PlantTree();
            }
        }
    }

    public void PlantTree()
    {
        isPlanted = true;
        // Spawn pohon di tengah petak; TreeGrowth otomatis memulainya dari ukuran kecil
        currentTree = Instantiate(treePrefab, transform.position, Quaternion.identity);
    }

    public void ClearPlot()
    {
        isPlanted = false;
        if (currentTree != null)
        {
            Destroy(currentTree);
        }
    }
}