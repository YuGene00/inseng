using UnityEngine;
using System.Collections;

public class Basket : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("basket");
    }
}
