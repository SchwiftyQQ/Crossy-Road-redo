using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using Assets.Scripts.Ui;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IPlayer
    {
        GameEvents events;


        public event Action Died;

        #region TweenParams
        [SerializeField] float jumpPower;
        [SerializeField] int numberOfJumps = 1;
        [SerializeField] float animDuration;
        //[SerializeField] Ease animEase;
        #endregion

        #region Directions
        private Vector3 left;
        private Vector3 right;
        private Vector3 forward;
        private Vector3 back;
        #endregion

        Vector3 previousPosition;
        int allowedKeyPress = 1;

        private void Start()
        {
            events = GameEvents.Instance;

            
            events.onTreeTriggerEnter += BounceOffTrees;
        }

        private void Update()
        {
            UpdateDirections();
            MovementKeyboard();
        }

        private void MovementKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.W) && allowedKeyPress == 1)
            {
                JumpMove(this.transform, forward, jumpPower, numberOfJumps, animDuration);

                GameEvents.Instance.OnMoreTerrainSpawn(transform.position);
            }

            if (Input.GetKeyDown(KeyCode.S) && allowedKeyPress == 1)
            {
                JumpMove(this.transform, back, jumpPower, numberOfJumps, animDuration);
            }

            if (Input.GetKeyDown(KeyCode.D) && allowedKeyPress == 1)
            {
                JumpMove(this.transform, right, jumpPower, numberOfJumps, animDuration);
            }

            if (Input.GetKeyDown(KeyCode.A) && allowedKeyPress == 1)
            {
                JumpMove(this.transform, left, jumpPower, numberOfJumps, animDuration);
            }
        }

        private void UpdateDirections()
        {
            if (transform.parent == null) {
                left = new Vector3(Mathf.Round(transform.position.x - 1), transform.position.y, transform.position.z);
                right = new Vector3(Mathf.Round(transform.position.x + 1), transform.position.y, transform.position.z);
                forward = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z + 1));
                back = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z - 1));
            }

            else if (transform.parent != null)
            {
                left = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                right = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                forward = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z + 1));
                back = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z - 1));
            }
        }

        void JumpMove(Transform objToMove, Vector3 dir, float jumpPower, int numJumps, float duration)
        {

            previousPosition = transform.position;

            allowedKeyPress -= 1;

            objToMove.DOJump(dir, jumpPower, numJumps, duration).OnComplete(ReplanishKeyPress);

            jumpPower = 1;
        }

        private void ReplanishKeyPress()
        {
            allowedKeyPress += 1;
        }

        private void BounceOffTrees(Collider other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                Debug.Log("bouncing off");
                DOTween.Kill(transform);
                transform.DOJump(previousPosition, jumpPower, numberOfJumps, animDuration).OnComplete(ReplanishKeyPress);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponentInParent<Water>() || other.gameObject.GetComponentInParent<CarMovement>())
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<LogMovement>())
            {
                gameObject.transform.SetParent(collision.transform.parent);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<LogMovement>())
            {
                gameObject.transform.SetParent(null);
            }
        }

        private void OnDisable()
        {
            Died?.Invoke();
        }

    }
    

}
