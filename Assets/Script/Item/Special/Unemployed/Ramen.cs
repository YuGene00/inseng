using UnityEngine;
using System.Collections;

public class Ramen : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(80);
        SetItemNumber(2);
        ownObjectPool.Release(this.gameObject);
    }
}