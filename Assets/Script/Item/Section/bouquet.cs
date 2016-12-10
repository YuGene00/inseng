using UnityEngine;
using System.Collections;

public class bouquet : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(300);
        SetItemNumber(1);
        ownObjectPool.Release(this.gameObject);
    }
}
