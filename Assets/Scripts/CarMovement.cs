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
            // RV: Unsubscribe from it at OnDisable.
            // Always care about event unsubscriptions.
            // Try to handle unsubscription right exactly when you've written subscription code.
            // BTW, I see bug here already: this object can be pooled.
            // If OnMovingObjectSpawn not called when an object was alive, unsubscription won't be performed.
            // This means that it subscribes on an event every time when it's retained from a pool.
            // This means that event handler would be called as many times as subscriptions happened.
            events.OnMovingObjectSpawn += MoveCar;
        }

        private void MoveCar(float moveEndValue)
        {
            // RV: I think that you can remove this check as soon
            // as you unsubscribe from OnMovingObjectSpawn at OnDisable
            if (gameObject.activeInHierarchy && gameObject != null)
            {
                events.OnMovingObjectSpawn -= MoveCar;
                transform.DOMoveX(moveEndValue, duration).SetEase(Ease.Linear).OnComplete(DisableObject);
            }
        }

        // RV: Inconsistent visibility modifiers
        void DisableObject()
        {
            gameObject.SetActive(false);
        }

        // RV: Also it's easier to read code when Unity callbacks are placed together.
        // You can use this order of members:
        // Fields
        // Properties
        // Unity callbacks
        // Public methods
        // Protected methods
        // Private methods
        // Event handlers
        private void OnDisable()
        {
            pools.ReturnObjectToPool(gameObject, "Car");
            DOTween.Kill(transform);
        }

    }
}
