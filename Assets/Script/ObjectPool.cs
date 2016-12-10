using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool {

    //struct
    struct Root {
        public MonoBehaviour mono;
        public Transform trans;
    }

    //variable
    Stack<GameObject> poolStack;
    Root root;
    GameObject origin;
    int unitNo;

    static public ObjectPool MakePoolOfObjWithNumber(GameObject obj, int unitNo = 20) {
        ObjectPool objectPool = new ObjectPool();
        objectPool.SetObject(obj);
        objectPool.SetUnitNumber(unitNo);
        objectPool.SetRoot();
        objectPool.AllocateMemory(unitNo);
        return objectPool;
    }

    ObjectPool() {
        poolStack = new Stack<GameObject>();
    }

    void SetObject(GameObject obj) {
        origin = obj;
    }

    void SetUnitNumber(int unitNo) {
        this.unitNo = unitNo;
    }

    void SetRoot() {
        GameObject rootObj = new GameObject(origin.name);
        root.mono = rootObj.GetComponent<MonoBehaviour>();
        root.trans = rootObj.transform;
    }

    void AllocateMemory(int allocateNo) {
        for (int i = 0; i < allocateNo; ++i) {
            GameObject newObj = MakeNewObjectIntoStack();
            newObj.SetActive(false);
        }
    }

    GameObject MakeNewObjectIntoStack() {
        GameObject newObj = MonoBehaviour.Instantiate(origin);
        newObj.transform.SetParent(root.trans);
        poolStack.Push(newObj);
        return newObj;
    }

    public GameObject Retain(Vector3 position = default(Vector3), Quaternion? rotation = null) {
        if (rotation == null) {
            rotation = Quaternion.identity;
        }

        if (IsStackEmpty()) {
            AllocateMemory(unitNo);
        }

        GameObject retainedObj = GetObjectFromStackTo(position, rotation.Value);
        retainedObj.SetActive(true);
        return retainedObj;
    }

    bool IsStackEmpty() {
        return poolStack.Count == 0;
    }

    GameObject GetObjectFromStackTo(Vector3 position, Quaternion rotation) {
        GameObject obj = poolStack.Pop();
        Transform trans = obj.transform;
        trans.position = position;
        trans.rotation = rotation;
        return obj;
    }

    public void Release(GameObject obj) {
        obj.SetActive(false);
        poolStack.Push(obj);
    }

    public void Release(GameObject obj, float time) {
        root.mono.StartCoroutine(ReleaseAfterTime(obj, time));
    }

    IEnumerator ReleaseAfterTime(GameObject obj, float time) {
        yield return new WaitForSeconds(time);
        Release(obj);
    }
}
