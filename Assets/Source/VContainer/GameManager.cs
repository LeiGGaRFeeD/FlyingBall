using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VContainer;
using VContainer.Unity;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemySpawner _enemySpawner;

    [Inject]
    public void Construct(PlayerController playerController, EnemySpawner enemySpawner)
    {
        _playerController = playerController;
        _enemySpawner = enemySpawner;
    }

    void Start()
    {
        _playerController.Initialize();
        _enemySpawner.SpawnEnemies();
    }
}
