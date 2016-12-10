using UnityEngine;
using System.Collections;
using System;

public class Ball : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("ball");
    }
}
