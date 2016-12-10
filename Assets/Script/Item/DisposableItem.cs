using UnityEngine;
using System.Collections;

public abstract class DisposableItem : Item {

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        
    }
}
