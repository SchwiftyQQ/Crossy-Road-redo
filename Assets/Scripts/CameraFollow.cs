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
        public float smoothness = 0.3f;

        private Vector3 velocity = Vector3.zero;


        // Update is called once per frame
        void Update()
        {

            if (player != null)
            {
                Vector3 pos = new Vector3();
                pos.x = player.gameObject.transform.position.x;
                pos.y = player.gameObject.transform.position.y + 2f;
                pos.z = player.gameObject.transform.position.z;
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);
            }
            else if (player == null)
            {
                player = (Player)GameManager.Instance.Player;
                return;
            }
        }
    }
}
