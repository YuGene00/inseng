using UnityEngine;
using System.Collections;

public class Runner : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Runner");
    }
}