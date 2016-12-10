using UnityEngine;
using System.Collections;

public class SelfTeachingBook : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("SelfTeachingBook");
    }
}