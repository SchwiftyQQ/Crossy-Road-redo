using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class CarMovement : MonoBehaviour
    {
        GameEvents events;

        [SerializeField] float duration;

        private void Awake()
        {
            events = GameEvents.Instance;
        }

        private void OnEnable()
        {
            GameEvents.Instance.onMovingObjectSpawn += MoveCar;
        }

        private void MoveCar(float moveEndValue)
        {
            if (gameObject.activeInHierarchy)
            {
                events.onMovingObjectSpawn -= MoveCar;
                transform.DOMoveX(moveEndValue, duration).SetEase(Ease.Linear).OnComplete(DisableObject);
            }
        }
        
        void DisableObject()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ObjectPooler.Instance.ReturnObjectToPool(gameObject, "Car");
        }

    }
}
