using UnityEngine;
using System.Collections;

public class Salary : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(300);
        SetItemNumber(1);
        ownObjectPool.Release(this.gameObject);
    }
}