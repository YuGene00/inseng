using UnityEngine;
using System.Collections;

public class DrawingPaper : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(100);
        SetItemNumber(1);
        ownObjectPool.Release(this.gameObject);
    }
}