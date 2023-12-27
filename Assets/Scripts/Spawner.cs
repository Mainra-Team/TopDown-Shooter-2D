using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; // Gunakan List atau Array untuk menyimpan objek yang akan di-spawn
    public float initialSpawnInterval = 3f;
    public bool useExponentialInterval = false;
    public float minExponentialInterval = 1f;

    private Camera mainCamera;
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        mainCamera = Camera.main;
        navMeshSurface = FindObjectOfType<NavMeshSurface>();

        // Memanggil Coroutine untuk memulai spawn objek
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        float spawnInterval = initialSpawnInterval;

        while (true)
        {
            // Mendapatkan ukuran layar dalam satuan world space
            float screenX = Random.Range(-0.1f, 1.1f);
            float screenY = Random.Range(-0.1f, 1.1f);

            Vector3 spawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(screenX, screenY, mainCamera.nearClipPlane));
            spawnPoint.z = 0f;

            // Cek apakah spawnPoint berada di dalam area NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPoint, out hit, 5f, NavMesh.AllAreas))
            {
                // Pilih objek secara acak dari array atau list objectsToSpawn
                GameObject selectedObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
                Instantiate(selectedObject, hit.position, Quaternion.identity);
            }

            // Atur ulang interval spawn berdasarkan opsi useExponentialInterval
            if (useExponentialInterval)
            {
                spawnInterval = Mathf.Max(minExponentialInterval, spawnInterval * 0.9f);
            }

            // Tunggu sebelum spawn objek berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
