using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemNeedAddInPool
{
    public GameObject obj;
    public int quantity;
}
public class PoolManager : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]public static PoolManager instance;
    [HideInInspector]public List<GameObject> pools = new List<GameObject>();
    [SerializeField] List<ItemNeedAddInPool> ItemPools = new List<ItemNeedAddInPool>();
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    private void Start() {
        AddItemPools();
    }
    void AddItemPools(){
        foreach(var item in ItemPools){
            for(int i=0; i<item.quantity;i++){
                var objAdd = Instantiate(item.obj);
                pools.Add(objAdd);
                objAdd.transform.SetParent(transform);
                objAdd.SetActive(false);
            }
        }
    }
    public GameObject GetObj(string name){
        foreach(var item in pools){
            if(item.name == name && !item.activeInHierarchy){
                // item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public void Add(GameObject obj, int quantity){
        for(int i=0; i<quantity;i++){
            obj.SetActive(false);
            pools.Add(obj);
        }
    }

    internal GameObject GetObj(object hit_Effect)
    {
        throw new NotImplementedException();
    }
}

