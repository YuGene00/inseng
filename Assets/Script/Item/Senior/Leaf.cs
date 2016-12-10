using UnityEngine;
using System.Collections;

public class Leaf : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Leaf");
    }
}