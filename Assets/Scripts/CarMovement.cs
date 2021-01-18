using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class CarMovement : MonoBehaviour
    {
        GameEvents events;
        ObjectPooler pools;

        [SerializeField] float duration;

        private void Awake()
        {
            events = GameEvents.Instance;
            pools = ObjectPooler.Instance;
        }

        private void OnEnable()
        {
            events.OnMovingObjectSpawn += MoveCar;
        }

        private void MoveCar(float moveEndValue)
        {
            if (gameObject.activeInHierarchy && gameObject != null)
            {
                events.OnMovingObjectSpawn -= MoveCar;
                transform.DOMoveX(moveEndValue, duration).SetEase(Ease.Linear).OnComplete(DisableObject);
            }
        }
        
        void DisableObject()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            pools.ReturnObjectToPool(gameObject, "Car");
            DOTween.Kill(transform);
        }

    }
}
