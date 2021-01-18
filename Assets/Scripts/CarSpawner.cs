using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarSpawner : ObjectSpawner
    {
        [SerializeField] BoxCollider transitLine;


        [SerializeField] float minSpawnTime;
        [SerializeField] float maxSpawnTime;


        private void OnEnable()
        {
            StartCoroutine(SpawnAMovingObject("Car", transitLine, minSpawnTime, maxSpawnTime, transform));
            
        }
        
    }
}
