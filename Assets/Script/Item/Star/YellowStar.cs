using UnityEngine;
using System.Collections;

public class YellowStar : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(10);
        ownObjectPool.Release(this.gameObject);
    }
}