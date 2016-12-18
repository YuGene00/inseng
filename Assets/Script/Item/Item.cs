using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    //caching
    Transform trans;
    ObjectPool ownObjectPool = null;
    public ObjectPool OwnObjectPool {
        get {
            return ownObjectPool;
        }
        set {
            ownObjectPool = value;
        }
    }

    //variable
    Move move;
    Vector2 unitDistance = Vector2.zero;
    string itemName = null;
    public string ItemName {
        get {
            return itemName;
        }
        set {
            itemName = value;
        }
    }
    EffectorManager effectorManager = new EffectorManager(5);

    void Awake() {
        Caching();
        InitMove();
    }

    void Caching() {
        trans = transform;
    }

    void InitMove() {
        move = new Move();
        move.SetMovableArea(new Vector2(-460f, -740f), new Vector2(460f, 840f));
    }

    void OnEnable() {
        StartCoroutine("DropItem");
    }

    IEnumerator DropItem() {
        while(true) {
            unitDistance.y = BackMover.Speed * Time.deltaTime;
            move.MoveTransToDest(trans, (Vector2)trans.position - unitDistance);
            DestroyIfOutOfArea();
            yield return null;
        }
    }

    void DestroyIfOutOfArea() {
        if(!move.IsInArea(trans.position)) {
            DestroyItem();
        }
    }

    public void DestroyItem() {
        ownObjectPool.Release(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        effectorManager.RunAllEffector();
    }

    public Item AddEffectorAndReturnItem(Effector effector) {
        effectorManager.AddEffector(effector, this);
        return this;
    }

    public void RemoveEffector(System.Type type, int value) {
        effectorManager.RemoveEffector(type, value);
    }
}