using UnityEngine;
using System.Collections;

public class Spine : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Spine");
    }
}