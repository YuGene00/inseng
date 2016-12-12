using UnityEngine;
using System.Collections;

public class Mask : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(-100);
        SetItemNumber(-1);
        ownObjectPool.Release(this.gameObject);
    }
}