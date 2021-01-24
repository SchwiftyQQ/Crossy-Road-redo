using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("This gets updated at runtime")]
        // RV: If this get updated at runtime, there is no need to serialize this field
        [SerializeField] private Player player;
        [SerializeField] float smoothness;

        private Vector3 velocity = Vector3.zero;
        private Vector3 pos = new Vector3();


        // Update is called once per frame
        // RV: Inconsistent visibility modifiers (implicit 'private')
        void LateUpdate()
        {
            // camera behavior
            if (player != null)
            {
                if (!GameManager.gameIsPaused && GameManager.Instance.Player.gameObject.activeInHierarchy)
                {
                    pos.x = player.gameObject.transform.position.x;
                    pos.y = player.gameObject.transform.position.y + 2f;

                    // slowly move camera forward so player has to move or die

                    // RV: You should use something that depends of deltaTime instead of direct addition
                    // in update methods. For example pos.z += overcomeSpeed * Time.deltaTime.
                    // This will make code more sensible and your game will behave correct on different frame rates.
                    // Also this will allow you to get rid of gameIsPaused checks.
                    // When timeScale == 0.0f, deltaTime == 0.0f too.
                    // That means that a product of any number with 0.0f is 0.0f too.
                    pos.z += 0.0055f;

                    // RV: You can pass deltaTime here to get rid of gameIsPaused check
                    transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);

                    // if bottom edge of the camera meets player's Z position, player dies
                    if (pos.z > (player.gameObject.transform.position.z + 6f))
                    {
                        // RV: It's not obvious that deactivating of a gameObject kills a player
                        player.gameObject.SetActive(false);
                    }

                    // if player goes too fast, camera speeds up until it centers on the player
                    else if (pos.z < (player.gameObject.transform.position.z - 3.5f))
                    {
                        Debug.Log($"pos.z = {pos.z}");
                        // RV: Again, use combination of some speed SerializedField and Time.deltaTime
                        pos.z += 0.015f;
                    }
                }
            }
            // RV: Condition can be safely removed, because upper 'if' condition
            // is an exact counterpart of this 'else if' condition
            else if (player == null)
            {
                // RV: Unneeded type cast
                player = (Player)GameManager.Instance.Player;
                // RV: Unneeded statement
                return;
            }


        }
    }
}
