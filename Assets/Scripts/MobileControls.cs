using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class MobileControls : PlayerMovement
    {

        #region SwipeParams
        public static float maxSwipeTime = 0.5f;
        public static float minSwipeDistance = 50f;

        private static float swipeStartTime;
        private static float swipeEndTime;
        private static float swipeTime;

        private static Vector2 startSwipePos;
        private static Vector2 endSwipePos;
        private static float swipeLength;
        #endregion


        // check for swipes
        public static bool PlayerHasSwiped()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    swipeStartTime = Time.time;
                    startSwipePos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    swipeEndTime = Time.time;
                    endSwipePos = touch.position;
                    swipeTime = swipeEndTime - swipeStartTime;
                    swipeLength = (endSwipePos - startSwipePos).magnitude;

                    if (swipeTime < maxSwipeTime && swipeLength > minSwipeDistance)
                    {
                        //SwipeControl();
                        return true;
                    }
                }
            }
            return false;
        }

        // give the method all the 4 possible directions, and it will do the job based on which direction player swipes
        public static void SwipeControl(Transform objToMove, Vector3 left, Vector3 right, Vector3 forward, Vector3 back, float jumpPower, int numJumps, float duration)
        {
            Vector2 distance = endSwipePos - startSwipePos;
            float xDistance = Mathf.Abs(distance.x);
            float yDistance = Mathf.Abs(distance.y);

            if (xDistance > yDistance)
            {
                if (distance.x > 0 && allowedKeyPress == 1)
                {
                    if (Player.IsOnLog)
                    {
                        // move right on log
                        // if on log, execute version of JumpMove for "on log" movement
                        JumpMove(objToMove, Vector3.right);
                    }
                    else
                    {
                        // else, normal version
                        JumpMove(objToMove, right, jumpPower, numJumps, duration);
                    }
                }
                
                else if (distance.x < 0 && allowedKeyPress == 1)
                {
                    if (Player.IsOnLog)
                    {
                        JumpMove(objToMove, Vector3.left);
                    }
                    else
                    {
                        JumpMove(objToMove, left, jumpPower, numJumps, duration);
                    }
                }
            }

            if (xDistance < yDistance)
            {
                if (distance.y > 0 && allowedKeyPress == 1)
                {
                    JumpMove(objToMove, forward, jumpPower, numJumps, duration);
                }

                else if (distance.y < 0 && allowedKeyPress == 1)
                {
                    JumpMove(objToMove, back, jumpPower, numJumps, duration);
                }
            }
        }
    }
}