using UnityEngine;
using System.Collections;

public abstract class CoreItem : DisposableItem {

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        SetItemNumber(1);
    }
}