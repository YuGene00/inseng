using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    //caching
    Transform trans;

    //inspector
    public BackMover backMover;

    //variable
    Move move;
    Vector2 unitDistance = Vector2.zero;

    void Awake() {
        Caching();
        move = new Move();
    }

    void Caching() {
        trans = transform;
    }

    void OnEnable() {
        StartCoroutine("DropItem");
    }

    IEnumerator DropItem() {
        while(true) {
            unitDistance.y = 700f * Time.deltaTime;
            move.MoveTransformTo(trans, (Vector2)trans.position - unitDistance);
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        EatenByPlayer();
    }

    protected abstract void EatenByPlayer();
}
