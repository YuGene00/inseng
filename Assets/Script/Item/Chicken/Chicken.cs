using UnityEngine;
using System.Collections;

public class Chicken : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Chicken");
    }
}