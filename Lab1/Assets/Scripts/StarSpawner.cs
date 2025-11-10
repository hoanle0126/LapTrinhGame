using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public float spawnRate = 5f; // Sao xuất hiện sau mỗi 5 giây
    public float spawnRange = 8f; // Phạm vi ngẫu nhiên xung quanh Player

    private Transform playerTransform;
    private float nextSpawnTime = 0f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnStar();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnStar()
    {
        // Sinh ra Sao ngẫu nhiên gần vị trí Player/Camera
        Vector3 playerPos = playerTransform.position;

        // Tạo một offset ngẫu nhiên trong phạm vi 2D
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            0f // Giữ Z = 0 hoặc Z gần Player
        );

        Vector3 spawnPosition = playerPos + randomOffset;

        // Sinh ra ngôi sao
        Instantiate(starPrefab, spawnPosition, Quaternion.identity);
    }
}