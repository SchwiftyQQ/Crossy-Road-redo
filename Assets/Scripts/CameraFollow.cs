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
        [SerializeField] private Player player;
        public float smoothness = 0.6f;

        private Vector3 velocity = Vector3.zero;
        private Vector3 pos = new Vector3();


        // Update is called once per frame
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
                    pos.z += 0.004f;

                    transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);

                    // if bottom edge of the camera meets player's Z position, player dies
                    if (pos.z > (player.gameObject.transform.position.z + 6f))
                    {
                        player.gameObject.SetActive(false);
                    }

                    // if player goes too fast, camera speeds up until it centers on the player
                    else if (pos.z < (player.gameObject.transform.position.z - 3.5f))
                    {
                        Debug.Log($"pos.z = {pos.z}");
                        pos.z += 0.015f;
                    }
                }
            }

            else if (player == null)
            {
                player = (Player)GameManager.Instance.Player;
                return;
            }

            
        }
    }
}
