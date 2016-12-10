using UnityEngine;
using System.Collections;

public class Assignment : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(-80);
        SetItemNumber(-1);
        ownObjectPool.Release(this.gameObject);
    }
}