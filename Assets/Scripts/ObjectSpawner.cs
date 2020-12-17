using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public abstract class ObjectSpawner : MonoBehaviour
    {
        ObjectPooler pooler;
        GameEvents events;

        private void Start()
        {
            pooler = ObjectPooler.Instance;
            events = GameEvents.Instance;
        }

        protected IEnumerator SpawnAMovingObject(string poolTag, BoxCollider transitLine, float minSpawnRate, float maxSpawnRate, Transform parent)
        {
            Vector3[] spawnPoints = new Vector3[2];
            spawnPoints[0] = transitLine.bounds.min;
            spawnPoints[1] = transitLine.bounds.max;

            Vector3 randomSpawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];

            if (Equals(randomSpawnPos, spawnPoints[0]))
            {
                while (true)
                {
                    yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
                    GameObject obj = pooler.SpawnFromPool(poolTag, randomSpawnPos, Quaternion.identity);
                    obj.transform.SetParent(parent);
                    events.SpawnAMovingOBject(spawnPoints[1].x);
                };
            }

            else
            {
                while (true)
                {
                    yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
                    GameObject obj = pooler.SpawnFromPool(poolTag, randomSpawnPos, Quaternion.identity);
                    obj.transform.SetParent(parent);
                    events.SpawnAMovingOBject(spawnPoints[0].x);
                };
            }
        }

        protected IEnumerator SpawnTrees(string poolTag, BoxCollider treeLine, Transform parent)
        {
            yield return new WaitForSeconds(Mathf.Epsilon);
            int randomIndex = Random.Range(4, 6);
            for (int i = 0; i < randomIndex; i++)
            {
                Vector3 pos = new Vector3(Mathf.Round(Random.Range(treeLine.bounds.min.x, treeLine.bounds.max.x)), treeLine.transform.position.y, treeLine.transform.position.z);
                GameObject obj = pooler.SpawnFromPool(poolTag, pos, Quaternion.identity);
                obj.transform.SetParent(parent);
            }
        }

    }
}
