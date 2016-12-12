using UnityEngine;
using System.Collections;

public class RedStar : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(50);
        ownObjectPool.Release(this.gameObject);
    }
}
