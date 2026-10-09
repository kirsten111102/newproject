using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    public void spawnEnemy()
    {
        Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}
