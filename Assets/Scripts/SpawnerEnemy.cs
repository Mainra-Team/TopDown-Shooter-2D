using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 3f;

    private Camera mainCamera;
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        mainCamera = Camera.main;
        navMeshSurface = FindObjectOfType<NavMeshSurface>();

        // Memulai pemanggilan fungsi SpawnObject setiap spawnInterval detik
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        // Mendapatkan ukuran layar dalam satuan world space
        float screenX = Random.Range(0f, 1f);
        float screenY = Random.Range(0f, 1f);

        Vector3 spawnPoint = mainCamera.ViewportToWorldPoint(new Vector3(screenX, screenY, mainCamera.nearClipPlane));
        spawnPoint.z = 0f; // Pastikan objek spawn di bidang yang benar

        // Cek apakah spawnPoint berada di dalam area NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint, out hit, 5f, NavMesh.AllAreas))
        {
            Instantiate(objectToSpawn, hit.position, Quaternion.identity);
        }
    }
}
