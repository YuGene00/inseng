using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    //caching
    Transform trans;

    //variable
    Move move;
    Vector2 unitDistance = Vector2.zero;
    protected ObjectPool ownObjectPool;
    int score;

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

    public void SetOwnObjectPool(ObjectPool objPool) {
        ownObjectPool = objPool;
    }

    IEnumerator DropItem() {
        while(true) {
            unitDistance.y = GameController.GetInstance().gameSpeed * Time.deltaTime;
            move.MoveTransToDest(trans, (Vector2)trans.position - unitDistance);
            DestroyOutOfArea();
            yield return null;
        }
    }

    void DestroyOutOfArea() {
        if(!move.IsInArea(trans.position)) {
            ownObjectPool.Release(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        EatenByPlayer();
    }

    protected abstract void EatenByPlayer();

    protected void SetScore(int score) {
        GameController.GetInstance().score += score;
    }

    protected void SetItemNumber(int number) {
        GameController.GetInstance().EatItemNumber += number;
    }
}
