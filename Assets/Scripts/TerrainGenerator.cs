using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] int distanceFromPlayer;
        [SerializeField] int startingTerrainAmount = 20;


        Vector3 startingTerrainSpawnPos = new Vector3(0f, 0f, 0f);
        Vector3 terrainSpawnPos = new Vector3(0f, 0f, 0f);


        string[] pooledTerrainNames = new string[] { "Grass", "Road", "Water" };
        List<GameObject> currentTerrain = new List<GameObject>();

        private void Awake()
        {
            GameEvents.Instance.onMoreTerrainSpawn += SpawnAndDespawnTerrain;
        }

        void Start()
        {
            terrainSpawnPos.z = startingTerrainAmount;

            Vector3 terrainPos = new Vector3(0f, 0f, 0f);

            for (int i = 0; i < startingTerrainAmount; i++)
            {
                string randomPoolName = pooledTerrainNames[Random.Range(0, pooledTerrainNames.Length)];
                switch (randomPoolName)
                {
                    case "Grass":
                        terrainPos = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                    case "Water":
                        terrainPos = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                    case "Road":
                        terrainPos = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                }

                GameObject startingTerrain = ObjectPooler.Instance.SpawnFromPool(randomPoolName, terrainPos, Quaternion.identity);
                currentTerrain.Add(startingTerrain);

                startingTerrainSpawnPos.z++;
            }
        }

        public void SpawnAndDespawnTerrain(Vector3 playerPos)
        {
            if (terrainSpawnPos.z - Mathf.Round(playerPos.z) < distanceFromPlayer)
            {
                string randomPoolName = pooledTerrainNames[Random.Range(0, pooledTerrainNames.Length)];

                //for (int i = 1; i < Random.Range(1, 2); i++)
                //{
                    GameObject generatedTerrain = ObjectPooler.Instance.SpawnFromPool(randomPoolName, terrainSpawnPos, Quaternion.identity);
                    currentTerrain.Add(generatedTerrain);

                    terrainSpawnPos.z++;
                //}
                ObjectPooler.Instance.ReturnObjectToPool(currentTerrain[0], randomPoolName);
                currentTerrain[0].gameObject.SetActive(false);
                currentTerrain.RemoveAt(0);
            }
        }
    }
}
