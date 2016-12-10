using UnityEngine;
using System.Collections;

public class Stock : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Stock");
    }
}