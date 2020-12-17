using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LogSpawner : ObjectSpawner
    {
        [SerializeField] BoxCollider logTransitLine;


        [SerializeField] float minSpawnTime;
        [SerializeField] float maxSpawnTime;

        private void OnEnable()
        {
            StartCoroutine(SpawnAMovingObject("Log", logTransitLine, minSpawnTime, maxSpawnTime, transform));
        }
    }
}