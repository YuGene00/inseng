using UnityEngine;
using System.Collections;

public class Mask : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Mask");
    }
}