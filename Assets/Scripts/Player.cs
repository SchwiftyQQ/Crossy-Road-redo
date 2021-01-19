using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using Assets.Scripts.Ui;

namespace Assets.Scripts
{
    public class Player : PlayerMovement
    {
        GameEvents events;

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


        public static bool IsOnLog { get; private set; } = false;

        private void Start()
        {
            events = GameEvents.Instance;
        }

        private void LateUpdate()
        {
            MovementKeyboard();
            MovementMobile();
            UpdateDirections();
        }

        private void MovementKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.W) && allowedKeyPress == 1)
            {
                JumpMove(transform, forward, jumpPower, numberOfJumps, animDuration);

                events.GenerateTerrain(transform.position);

                this.Wait(animDuration, ScoreConditionsCheck);

            }

            if (Input.GetKeyDown(KeyCode.S) && allowedKeyPress == 1)
            {
                JumpMove(transform, back, jumpPower, numberOfJumps, animDuration);

            }

            if (Input.GetKeyDown(KeyCode.D) && allowedKeyPress == 1)
            {
                if (IsOnLog)
                    JumpMove(transform, Vector3.right);
                else
                    JumpMove(transform, right, jumpPower, numberOfJumps, animDuration);
            }

            if (Input.GetKeyDown(KeyCode.A) && allowedKeyPress == 1)
            {
                if (IsOnLog)
                    JumpMove(transform, Vector3.left);
                else
                    JumpMove(transform, left, jumpPower, numberOfJumps, animDuration);
            }
        }

        private void MovementMobile()
        {
            if (MobileControls.PlayerHasSwiped() && allowedKeyPress == 1)
            {
                MobileControls.SwipeControl(transform, left, right, forward, back, jumpPower, numberOfJumps, animDuration);
                
                events.GenerateTerrain(transform.position);

                this.Wait(animDuration, ScoreConditionsCheck);
            }
        }

        private void UpdateDirections()
        {
            left = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            right = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            forward = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z + 1));
            back = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z - 1));
        }

        private void BounceOffTrees()
        {
            allowedKeyPress = 1;
            Debug.Log("bouncing off");
            DOTween.Kill(transform);
            JumpMove(transform, previousPosition, jumpPower, numberOfJumps, animDuration);
        }

        private void ScoreConditionsCheck()
        {
            // checks the highest previous Z position from the list, and if next position is higher, increases score
            float maxZpos = Mathf.Max(prevZpositions.ToArray());

            if (transform.position.z > maxZpos)
            {
                GameEvents.Instance.OnPlayerMovedUp();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<CarMovement>()
            ||  other.GetComponentInParent<Water>())           
            { 
                gameObject.SetActive(false);
            }

            else if (other.GetComponentInParent<TreeBehaviour>())
            {
                BounceOffTrees();
            }

            else if (other.GetComponentInParent<TreeSpawner>() && other.GetType() == typeof(CapsuleCollider))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<LogMovement>())
            {
                gameObject.transform.SetParent(collision.transform);
                IsOnLog = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponentInParent<LogMovement>())
            {
                gameObject.transform.SetParent(null);
                IsOnLog = false;
            }
        }

        private void OnDisable()
        {
            events.OnPlayerDied();

            prevZpositions.Clear();

            IsOnLog = false;
        }

    }
    

}
