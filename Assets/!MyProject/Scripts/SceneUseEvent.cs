using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneUseEvent : MonoBehaviour
{

    [Header("Кнопка")]
    [SerializeField] private Button StartEventButton;
    [SerializeField] private Button ChangeColorButton;

    [Header("Позиция спавна")]
    [SerializeField] private Transform SpawnPosition;

    [Header("Префабы")]
    [SerializeField] private GameObject Prefab1;
    [SerializeField] private GameObject Prefab2;
    [SerializeField] private GameObject Prefab3;
    //[SerializeField] private Renderer Render1;
    //[SerializeField] private Renderer Render2;
    //[SerializeField] private Renderer Render3;

    [Header("Настройка спавна области")]
    [SerializeField] private float SpawnRadius = 3f;

    [Header("Настройки кнопки")]
    public Color CustomColor1 = Color.blue;
    public Color CustomColor2 = Color.green;
    public Color CustomColor3 = Color.white;
    private Coroutine ChangeColorCoroutine;

    public List<GameObject> PrefabSceneSpawned = new List<GameObject>();


    void Start()
    {
        ChangeColorButton.interactable = false;
    }

    public void StartEventSystemes()
    {
        if (SpawnPosition != null)
        {
            SpawnPrefabAll();
            StartEventButton.interactable = false;
            ChangeColorButton.interactable = true;
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

    public void ChangeColorObjects()
    {
        ChangeColorButton.interactable = false;

        ChangeColorCoroutine = StartCoroutine(ColorChangerMaterial());
    }

    private IEnumerator ColorChangerMaterial()
    {
        List<Renderer> renderers = new List<Renderer>();

        foreach (GameObject obj in PrefabSceneSpawned)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderers.Add(renderer);
            }
        }

        if (renderers.Count >= 3)
        {
            renderers[0].material.color = CustomColor1;
            renderers[1].material.color = CustomColor2;
            renderers[2].material.color = CustomColor3;
        }

        yield return new WaitForSeconds(5f);

        ChangeColorButton.interactable = true;
        ChangeColorCoroutine = null;
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
    