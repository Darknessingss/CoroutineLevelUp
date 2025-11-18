using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneUseEvent : MonoBehaviour
{

    private Button StartEventButton;

    [Header("Позиция спавна")]
    [SerializeField] private Transform SpawnPosition;

    [Header("Префабы")]
    [SerializeField] private GameObject Prefab1;
    [SerializeField] private GameObject Prefab2;
    [SerializeField] private GameObject Prefab3;

    [Header("Настройка спавна области")]
    [SerializeField] private float SpawnRadius = 3f;


    void Start()
    {

        if(StartEventButton != null)
        {
            StartEventButton.onClick.AddListener(StartEventSystemes);
            Debug.Log("Кнопка найдена, слушатель добавлен");
        }
        else
        {
            Debug.LogError("Кнопка не назначена в инспекторе!");
        }
    }

    public void StartEventSystemes()
    {
        if (SpawnPosition != null)
        {
            SpawnPrefabAll();
            StartEventButton.interactable = false;
        }
    }

    public void SpawnPrefabAll()
    {
        GameObject[] AllPrefabs = { Prefab1, Prefab2, Prefab3 };

        foreach(GameObject Prefab in AllPrefabs)
        {
            if(Prefab != null)
            {
                Vector3 RandomPositionSpawn = GetRandomPositionInRadius();
                Instantiate(Prefab, RandomPositionSpawn, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPositionInRadius()
    {
        Vector2 RandomCircle = Random.insideUnitCircle * SpawnRadius;
        Vector3 RandomOffset = new Vector3(RandomCircle.x, 0f, RandomCircle.y);
        return SpawnPosition.position + RandomOffset;
    }   




    private void OnDestroy()
    {
        if(StartEventButton != null)
        {
            StartEventButton.onClick.RemoveListener(StartEventSystemes);
        }
    }
}
    