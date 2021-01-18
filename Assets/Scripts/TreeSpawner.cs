using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TreeSpawner : ObjectSpawner
    {
        [SerializeField] BoxCollider treeLine;
        [SerializeField] List<Vector3> previousPositions;


        private void OnEnable()
        {
            StartCoroutine(SpawnTrees("Tree", previousPositions, treeLine, transform));
        }

    }
}
