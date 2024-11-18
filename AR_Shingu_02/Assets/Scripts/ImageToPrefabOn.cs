using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageToPrefabOn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs; // 여러 프리팹을 인스펙터에 추가

    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // 프리팹 딕셔너리 생성
        foreach (GameObject prefab in prefabs)
        {
            prefabDictionary[prefab.name] = prefab;
        }
    }

    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            SpawnPrefabForImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdatePrefabForImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            RemovePrefabForImage(trackedImage);
        }
    }

    private void SpawnPrefabForImage(ARTrackedImage trackedImage)
    {
        if (prefabDictionary.TryGetValue(trackedImage.referenceImage.name, out GameObject prefabToSpawn))
        {
            if (!spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
            {
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedPrefab.transform.parent = trackedImage.transform;

                // PrefabClickHandler 추가 (여기서 URL은 PrefabClickHandler에서 관리됨)
                PrefabClickHandler clickHandler = spawnedPrefab.AddComponent<PrefabClickHandler>();
                clickHandler.youtubeURL = "https://www.youtube.com/watch?v=YOUR_VIDEO_ID"; // URL을 여기에 설정

                spawnedPrefabs[trackedImage.referenceImage.name] = spawnedPrefab;
            }
        }
        else
        {
            Debug.LogWarning("No prefab found for image: " + trackedImage.referenceImage.name);
        }
    }

    private void UpdatePrefabForImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            GameObject spawnedPrefab = spawnedPrefabs[trackedImage.referenceImage.name];
            spawnedPrefab.transform.position = trackedImage.transform.position;
            spawnedPrefab.transform.rotation = trackedImage.transform.rotation;
        }
    }

    private void RemovePrefabForImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            Destroy(spawnedPrefabs[trackedImage.referenceImage.name]);
            spawnedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}