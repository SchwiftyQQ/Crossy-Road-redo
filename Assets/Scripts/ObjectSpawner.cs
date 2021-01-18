using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public abstract class ObjectSpawner : MonoBehaviour
    {
        ObjectPooler pooler;
        GameEvents events;


        Vector3 newPosition;

        private void Start()
        {
            pooler = ObjectPooler.Instance;
            events = GameEvents.Instance;
            
        }

        //had to make these into CoRoutines because without a "very small delay" they were not getting executed

        protected IEnumerator SpawnAMovingObject(string poolTag, BoxCollider transitLine, float minSpawnRate, float maxSpawnRate, Transform parent)
        {
            //BoxCollider is scaled down to a straight line and used as a transitLine for movingObjects
            //objects spawns at Bounds.min.x and moves to Bounds.max.x

            float randomXvalue = Random.Range(0, 2) == 0? transitLine.bounds.min.x : transitLine.bounds.max.x;
            if (randomXvalue == transitLine.bounds.min.x)
            {
                while (true)
                {
                    yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
                    GameObject obj = pooler.SpawnFromPool(poolTag, transitLine.bounds.min, Quaternion.identity);
                    obj.transform.SetParent(parent);
                    events.SpawnAMovingOBject(transitLine.bounds.max.x);
                };
            }

            else
            {
                while (true)
                {
                    yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
                    GameObject obj = pooler.SpawnFromPool(poolTag, transitLine.bounds.max, Quaternion.identity);
                    obj.transform.SetParent(parent);
                    events.SpawnAMovingOBject(transitLine.bounds.min.x);
                };
            }
        }

        protected IEnumerator SpawnTrees(string poolTag, List<Vector3> prevPositions, BoxCollider treeLine, Transform parent = null)
        {
            //similarly BoxCollider is used as scaled to a straight line and trees randomly spawn inside its bounds

            yield return new WaitForSeconds(Mathf.Epsilon);
            int randomIndex = Random.Range(4, 6);

            for (int j = 0; j < randomIndex; j++)
            {
                newPosition = new Vector3(Mathf.Round(Random.Range(treeLine.bounds.min.x, treeLine.bounds.max.x)), treeLine.transform.position.y, treeLine.transform.position.z);
                if (!prevPositions.Contains(newPosition))
                {
                    prevPositions.Add(newPosition);
                }
                else
                {
                    while (prevPositions.Contains(newPosition))
                    {
                        newPosition = new Vector3(Mathf.Round(Random.Range(treeLine.bounds.min.x, treeLine.bounds.max.x)), treeLine.transform.position.y, treeLine.transform.position.z);
                    }
                    prevPositions.Add(newPosition);
                }

                GameObject obj = pooler.SpawnFromPool(poolTag, prevPositions[j], Quaternion.identity);
                obj.transform.SetParent(parent);
            }
        }

    }
}
