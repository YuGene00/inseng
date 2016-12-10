using UnityEngine;
using System.Collections;

public class Superior : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(200);
        SetItemNumber(-1);
        ownObjectPool.Release(this.gameObject);
    }
}
