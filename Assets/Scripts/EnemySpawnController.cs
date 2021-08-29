using System;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    // editor fields
    public GameObject player;
    public GameObject enemyPrefab;
    public Transform enemyContainer;
    public int maxEnemyCount = 200;
    public float minSpawnFrequency = 15.0f;
    public float maxSpawnFrequency = 2.0f;
    public float timeTillMaxFrequency = 600.0f;
    public float maxSpawnDistance = 30f;
    
    // private field
    private bool _canSpawn;
    private float _spawnFrequency;
    private float _startTime;

    public void Start()
    {
        _canSpawn = true;
        player ??= GameObject.FindWithTag("Player");
        _startTime = Time.time;
        
        EventSystem.Current.RegisterEventListener<OnPlayerDeathContext>((e) =>
        {
            gameObject.SetActive(false);
        });
    }

    public void Update()
    {
        if (_spawnFrequency < maxSpawnDistance)
        {
            _spawnFrequency = Mathf.Lerp(minSpawnFrequency, maxSpawnFrequency, (Time.time - _startTime) /
                                                                               timeTillMaxFrequency);
        }
        
        if (!_canSpawn) return;
        if (enemyContainer.childCount > maxEnemyCount) return;
        
        EventSystem.Current.CallbackAfter(Spawn, (int) (_spawnFrequency * 1000));
        _canSpawn = false;
    }

    private void Spawn()
    {
        _canSpawn = true;
        var validLocations = new List<Transform>();

        for (var i = 0; i < transform.childCount; i++)
        {
            if (Vector3.Distance(player.transform.position, transform.GetChild(i).transform.position) <
                maxSpawnDistance)
            {
                validLocations.Add(transform.GetChild(i));
            }
        }

        if (validLocations.Count < 1)
        {
            throw new Exception("NO VALID SPAWN LOCATION");
        }

        var rand = Random.Range(0, validLocations.Count);
        var spawnLocation = validLocations[rand];
        Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation, enemyContainer);
    }
}