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
        private void OnTriggerEnter(Collider other)
        {
            GameEvents.Instance.OnTreeTriggerEnter(other);
        }

        private void OnDisable()
        {
            ObjectPooler.Instance.ReturnObjectToPool(gameObject, "Tree");
        }

    }
}
