
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnPoint;

    public void SpawnSpecificEnemy(int enemyIndex)
    {
        if (enemyPrefabs == null ||
            enemyIndex < 0 ||
            enemyIndex >= enemyPrefabs.Length)
        {
            Debug.LogWarning("Invalid enemy index!");
            return;
        }

        SpawnEnemy(enemyPrefabs[enemyIndex]);
    }

    public void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }

        // Only choose valid prefab references.
        List<GameObject> validPrefabs = new List<GameObject>();

        foreach (GameObject prefab in enemyPrefabs)
        {
            if (prefab != null)
            {
                validPrefabs.Add(prefab);
            }
        }

        if (validPrefabs.Count == 0)
        {
            Debug.LogError(
                "All enemy prefab references are missing!"
            );
            return;
        }

        int randomIndex = Random.Range(0, validPrefabs.Count);
        SpawnEnemy(validPrefabs[randomIndex]);
    }

    private void SpawnEnemy(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("The selected enemy prefab is missing!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn Point is not assigned!");
            return;
        }

        Instantiate(
            prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}
