using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public struct MobileControls
    {

        #region SwipeParams
        public static float maxSwipeTime;
        public static float minSwipeDistance;

        private static float swipeStartTime;
        private static float swipeEndTime;
        private static float swipeTime;

        private static Vector2 startSwipePos;
        private static Vector2 endSwipePos;
        private static float swipeLength;
        #endregion



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


        public static void SwipeControl(bool allowedKeyPresses)
        {
            Vector2 distance = endSwipePos - startSwipePos;
            float xDistance = Mathf.Abs(distance.x);
            float yDistance = Mathf.Abs(distance.y);

            if (xDistance > yDistance)
            {
                if (distance.x > 0 && allowedKeyPresses)
                {
                    //MoveRight();
                }
                

                else if (distance.x < 0 && allowedKeyPresses)
                {
                    //MoveLeft();
                }
                

            }

            if (xDistance < yDistance)
            {
                if (distance.y > 0 && allowedKeyPresses)
                {
                    //MoveForward();
                }

                else if (distance.y < 0 && allowedKeyPresses)
                {
                    //MoveDown();
                }
            }
        }
    }
}