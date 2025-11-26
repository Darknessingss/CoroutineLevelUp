using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneUseEvent : MonoBehaviour
{

    [Header("������")]
    [SerializeField] private Button _startEventButton;
    [SerializeField] private Button _sortButton;
    [SerializeField] private Button _PhysBallButton;

    [Header("������� ������")]
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _spawnBall;

    [Header("�������")]
    [SerializeField] private GameObject _prefab1;
    [SerializeField] private GameObject _prefab2;
    [SerializeField] private GameObject _prefab3;
    [SerializeField] private GameObject _physicBall;

    [Header("��������� ������ �������")]
    [SerializeField] private float _spawnRadius = 3f;
    [SerializeField] private float _spacing = 2f;

    public void SortCubeScale()
    {
        GameObject[] PrefabScale = GameObject.FindGameObjectsWithTag("Prefabers");

        BubbleSortPrefabScale(PrefabScale);

        ArrangeCubesInLine(PrefabScale);

        _sortButton.interactable = false;
    }
    private void BubbleSortPrefabScale(GameObject[] PrefabScale)
    {
        int n = PrefabScale.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                float scale1 = PrefabScale[j].transform.localScale.x;
                float scale2 = PrefabScale[j + 1].transform.localScale.x;

                if (scale1 > scale2)
                {
                    GameObject Pref = PrefabScale[j];
                    PrefabScale[j] = PrefabScale[j + 1];
                    PrefabScale[j + 1] = Pref;
                }
            }
        }
    }
    private void ArrangeCubesInLine(GameObject[] PrefabScale)
    {
        Vector3 startPosition = _spawnPosition.position;

        for (int i = 0; i < PrefabScale.Length; i++)
        {
            float offset = i * _spacing;
            Vector3 newPosition = startPosition + Vector3.right * offset;

            PrefabScale[i].transform.position = newPosition;
        }
    }
    public void StartSpawnPrefabs()
    {
        if (_spawnPosition != null)
        {
            SpawnPrefabAll();
            _startEventButton.interactable = false;
        }
    }
    private void SpawnPrefabAll()
    {
        GameObject[] AllPrefabs = { _prefab1, _prefab2, _prefab3 };

        foreach (GameObject Prefab in AllPrefabs)
        {
            if (Prefab != null)
            {
                Vector3 RandomPositionSpawn = GetRandomPositionInRadius();
                Instantiate(Prefab, RandomPositionSpawn, Quaternion.identity);
            }
        }
    }
    public void SpawnPhysBall()
    {
        if (_spawnBall != null)
            SpawnBall();
                _PhysBallButton.interactable = false;
    }
    private void SpawnBall()
    {
        Vector3 SpawnBallPosition = _spawnBall.position;
        Instantiate(_physicBall, SpawnBallPosition, Quaternion.identity);
    }
    private Vector3 GetRandomPositionInRadius()
    {
        Vector2 RandomCircle = Random.insideUnitCircle * _spawnRadius;
        Vector3 RandomOffset = new Vector3(RandomCircle.x, 0f, RandomCircle.y);
        return _spawnPosition.position + RandomOffset;
    }   
}
    