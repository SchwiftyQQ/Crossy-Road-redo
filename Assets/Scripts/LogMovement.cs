using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class LogMovement : MonoBehaviour
    {
        GameEvents events;

        [SerializeField] float duration;

        private void Awake()
        {
            events = GameEvents.Instance;
        }

        private void OnEnable()
        {
            GameEvents.Instance.onMovingObjectSpawn += MoveLog;
        }

        private void MoveLog(float moveEndValue)
        {
            if (gameObject.activeInHierarchy)
            {
                events.onMovingObjectSpawn -= MoveLog;
                transform.DOMoveX(moveEndValue, duration).SetEase(Ease.Linear).OnComplete(DisableObject);
            }
        }

        void DisableObject()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ObjectPooler.Instance.ReturnObjectToPool(gameObject, "Log");
        }
    }
}