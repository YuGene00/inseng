using UnityEngine;
using System.Collections;

public class Document : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(200);
        SetItemNumber(2);
        ownObjectPool.Release(this.gameObject);
    }
}