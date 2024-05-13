using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour{
    [System.Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;
        public int size;
    }
    //pools리스트는 화살의 종류가 다들어갈꺼같어
    //pool class에 있는것 처럼 size를 정해주겠지
    public List<Pool> pools = new List<Pool>();//list는 기본적으로 serialize가 된다.
    //Serialize(직렬화) 데이터구조를 다른 형태로 변화하거나 저장가능
    public Dictionary<string, Queue<GameObject>> PoolDictionary;//얘는 직렬화 안되어있다.

    private void Awake(){
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools){
            Queue<GameObject> queue = new Queue<GameObject>();
            for(int i=0; i<pool.size;i++){
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag){
        if(!PoolDictionary.ContainsKey(tag)){
            return null;
        }

        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }
}