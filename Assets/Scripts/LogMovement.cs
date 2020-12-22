using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class LogMovement : MonoBehaviour
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
            events.OnMovingOBjectSpawn += MoveLog;
        }

        private void MoveLog(float moveEndValue)
        {
            if (gameObject.activeInHierarchy)
            {
                events.OnMovingOBjectSpawn -= MoveLog;
                transform.DOMoveX(moveEndValue, duration).SetEase(Ease.Linear).OnComplete(DisableObject);
            }
        }

        void DisableObject()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            pools.ReturnObjectToPool(gameObject, "Log");
        }
    }
}