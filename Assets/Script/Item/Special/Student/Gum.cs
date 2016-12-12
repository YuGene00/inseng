using UnityEngine;
using System.Collections;

public class Gum : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(-80);
        SetItemNumber(-1);
        ownObjectPool.Release(this.gameObject);
    }
}