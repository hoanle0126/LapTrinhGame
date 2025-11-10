using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 2f;
    public float spawnDistance = 15f;

    // ĐỊNH NGHĨA CÁC KÍCH THƯỚC TRÊN INSPECTOR
    [Header("Asteroid Sizes and Scales")]
    public float[] asteroidScales = { 2.0f, 1.2f, 0.8f, 0.5f }; // Big, Medium, Small, Tiny

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
            SpawnAsteroid(GetRandomSizeIndex()); // Sinh ra ngẫu nhiên
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    // Hàm trả về chỉ số kích thước ngẫu nhiên (0: Big, 1: Medium, 2: Small)
    // Loại trừ Tiny (index 3) vì nó chỉ nên được sinh ra khi vỡ.
    int GetRandomSizeIndex()
    {
        // Random từ 0 đến 2 (Big, Medium, Small)
        return Random.Range(0, asteroidScales.Length - 1);
    }

    // Hàm sinh thiên thạch, có thể gọi từ bên ngoài (Asteroid.cs)
    public void SpawnAsteroid(int sizeIndex, Vector3 position, Vector2 direction)
    {
        if (sizeIndex < 0 || sizeIndex >= asteroidScales.Length) return;

        float scaleValue = asteroidScales[sizeIndex];

        GameObject newAsteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);

        // Gán kích thước và chỉ số kích thước
        newAsteroid.transform.localScale = Vector3.one * scaleValue;

        // Cấu hình script Asteroid
        Asteroid asteroidScript = newAsteroid.GetComponent<Asteroid>();
        if (asteroidScript != null)
        {
            asteroidScript.Initialize(sizeIndex, direction);
        }
    }

    // Quá tải (Overload) hàm sinh ra thiên thạch ban đầu (từ ngoài màn hình)
    void SpawnAsteroid(int sizeIndex)
    {
        Vector3 playerPos = playerTransform.position;
        float angle = Random.Range(0f, 360f);
        Vector3 spawnDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f).normalized;
        Vector3 spawnPosition = playerPos + spawnDirection * spawnDistance;

        // Gọi hàm sinh chính
        SpawnAsteroid(sizeIndex, spawnPosition, (playerPos - spawnPosition).normalized);
    }
}