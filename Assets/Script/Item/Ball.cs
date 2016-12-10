using UnityEngine;
using System.Collections;
using System;

public class Ball : Item {

    protected override void EatenByPlayer() {
        Debug.Log("eat");
    }
}
