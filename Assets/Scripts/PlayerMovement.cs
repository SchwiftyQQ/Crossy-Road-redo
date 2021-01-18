using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public abstract class PlayerMovement : MonoBehaviour
    {
        //general previous position for the player
        protected static Vector3 previousPosition;
        //allowed key presses for restricting movement to animation duration
        protected static int allowedKeyPress = 1;

        //previous Z positions of the player;
        protected static List<float> prevZpositions = new List<float>(5);


        
        protected static void JumpMove(Transform objToMove, Vector3 dir, float jumpPower, int numJumps, float duration)
        {
            //Tweened Jump Move for regular movement

            if (allowedKeyPress == 1)
            {
                Debug.Log("Triggered");
                previousPosition = objToMove.transform.position;

                prevZpositions.Add(previousPosition.z);

                allowedKeyPress -= 1;

                objToMove.DOJump(dir, jumpPower, numJumps, duration).OnComplete(ReplanishKeyPresses);
            }
        }

        
        protected static void JumpMove(Transform objToMove, Vector3 dirForOnLogMov)
        {
            //movement on logs that float on water (no tween, needs animation for jumping (p.s. could make it, didn't bother))

            if (allowedKeyPress == 1)
            {
                if (objToMove.parent != null)
                {
                    objToMove.transform.position += dirForOnLogMov;
                }
            }
        }

        static void ReplanishKeyPresses()
        {
            allowedKeyPress += 1;
        }

        
    }
}