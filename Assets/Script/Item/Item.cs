using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour {

    //caching
    Transform trans;

    //variable
    Move move;
    Vector2 unitDistance = Vector2.zero;
    ObjectPool ownObjectPool;
    string itemName;
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
            unitDistance.y = GameController.GetInstance().gameSpeed * Time.deltaTime;
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
        ownObjectPool.Release(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        effectorManager.RunAllEffect();
    }

    public void SetOwnObjectPool(ObjectPool objPool) {
        ownObjectPool = objPool;
    }

    public Item AddEffectorAndReturnItem(Effector effector) {
        effectorManager.AddEffector(effector, this);
        return this;
    }

    public void RemoveEffector(System.Type type, int value) {
        effectorManager.RemoveEffector(type, value);
    }
}