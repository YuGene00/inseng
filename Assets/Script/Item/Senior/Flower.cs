using UnityEngine;
using System.Collections;

public class Flower : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Flower");
    }
}