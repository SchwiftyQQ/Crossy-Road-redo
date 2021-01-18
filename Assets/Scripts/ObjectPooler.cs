using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public class ObjectPooler : MonoBehaviour
    {
        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;


        #region quick Singleton
        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;

            
        }
        #endregion

        private void Start()
        {

            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                if (pool != null)
                {
                    Queue<GameObject> objectPool = new Queue<GameObject>();

                    for (int i = 0; i < pool.size; i++)
                    {
                        GameObject obj = Instantiate(pool.prefab);
                        obj.SetActive(false);
                        obj.transform.SetParent(transform);
                        objectPool.Enqueue(obj);
                    }
                    poolDictionary.Add(pool.tag, objectPool);
                }
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogError($"Pool with tag {tag}, doesn't exist");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = pos;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);

            return objectToSpawn;
        }

        public void ReturnObjectToPool(GameObject objectToReturn, string tag)
        {
            if (poolDictionary.ContainsKey(tag))
            {
                poolDictionary[tag].Enqueue(objectToReturn);
            }
        }

    }
}
