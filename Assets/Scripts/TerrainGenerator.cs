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
        const string GRASS = "Grass";
        const string WATER = "Water";
        const string ROAD = "Road";


        [Header("Distance from player that new terrain starts generating")]
        [SerializeField] int distanceFromPlayer;
        [Space]
        [SerializeField] int startingTerrainAmount;


        Vector3 startingTerrainSpawnPos = new Vector3(0f, 0f, 0f);
        Vector3 terrainSpawnPos = new Vector3(0f, 0f, 0f);


        readonly List<string> pooledTerrainNames = new List<string> (3) { "Grass", "Road", "Water" };
        readonly List<GameObject> currentTerrain = new List<GameObject>();


        string previousTerrainName = null;

        private void Awake()
        {
            GameEvents.Instance.OnTerrainGenerate += SpawnAndDespawnTerrain;
        }

        void Start()
        {
            terrainSpawnPos.z = startingTerrainAmount;

            GenerateStartingTerrain();
        }

        private void GenerateStartingTerrain()
        {
            // generate "startingTerrainAmount" of terrain on start

            Vector3 position = new Vector3(0f, 0f, 0f);
            for (int i = 0; i < startingTerrainAmount; i++)
            {
                // choose a random terrain between 3 prefabed terrains, and spawn them without repeating
                string randomPoolName = pooledTerrainNames[Random.Range(0, pooledTerrainNames.Count)];
                while (randomPoolName == previousTerrainName)
                {
                    randomPoolName = pooledTerrainNames[Random.Range(0, pooledTerrainNames.Count)];
                }

                switch (randomPoolName)
                {
                    case GRASS:
                        position = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                    case WATER:
                        position = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                    case ROAD:
                        position = new Vector3(0, 0, startingTerrainSpawnPos.z);
                        break;
                }

                GameObject startingTerrain = ObjectPooler.Instance.SpawnFromPool(randomPoolName, position, Quaternion.identity);
                previousTerrainName = randomPoolName;
                
                // adding terrain into a list so I can get and remove the last one and return in to the pool later, to reuse it.
                currentTerrain.Add(startingTerrain);

                // starting position's Z gets increased everytime so terrin spawns 1 point in front of the next
                startingTerrainSpawnPos.z++;
            }
        }

        public void SpawnAndDespawnTerrain(Vector3 playerPos)
        {
            if (terrainSpawnPos.z - Mathf.Round(playerPos.z) < distanceFromPlayer)
            {
                string randomPoolName = pooledTerrainNames[Random.Range(0, pooledTerrainNames.Count)];
                

                GameObject generatedTerrain = ObjectPooler.Instance.SpawnFromPool(randomPoolName, terrainSpawnPos, Quaternion.identity);
                currentTerrain.Add(generatedTerrain);

                terrainSpawnPos.z++;

                ObjectPooler.Instance.ReturnObjectToPool(currentTerrain[0], randomPoolName);
                currentTerrain[0].gameObject.SetActive(false);
                currentTerrain.RemoveAt(0);
            }
        }
    }
}
