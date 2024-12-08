using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // Регистрация ваших зависимостей
        builder.Register<PlayerController>(Lifetime.Scoped);
        builder.Register<EnemySpawner>(Lifetime.Scoped);
        builder.Register<ScoreManager>(Lifetime.Singleton);
        
        // Пример MonoBehaviour
        builder.RegisterComponentInHierarchy<GameManager>();
    }
}
