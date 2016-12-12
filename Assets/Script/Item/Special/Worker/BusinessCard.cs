using UnityEngine;
using System.Collections;

public class BusinessCard : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(200);
        SetItemNumber(3);
        ownObjectPool.Release(this.gameObject);
    }
}