using UnityEngine;
using System.Collections;

public class Leaf : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(100);
        SetItemNumber(1);
        ownObjectPool.Release(this.gameObject);
    }
}