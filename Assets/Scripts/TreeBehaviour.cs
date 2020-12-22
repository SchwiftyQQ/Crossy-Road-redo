using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TreeBehaviour : MonoBehaviour
    {
        GameEvents events;
        ObjectPooler pools;


        private void Awake()
        {
            events = GameEvents.Instance;
            pools = ObjectPooler.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            events.OnTreeTriggerEnter(other);
        }

        private void OnDisable()
        {
            pools.ReturnObjectToPool(gameObject, "Tree");
        }

    }
}
