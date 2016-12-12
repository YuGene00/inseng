using UnityEngine;
using System.Collections;

public class APlus : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(200);
        SetItemNumber(2);
        ownObjectPool.Release(this.gameObject);
    }
}