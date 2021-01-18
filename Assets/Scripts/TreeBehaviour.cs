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
        ObjectPooler pools;


        private void Awake()
        {
            pools = ObjectPooler.Instance;
        }

        private void OnDisable()
        {
            pools.ReturnObjectToPool(gameObject, "Tree");
        }

    }
}
