using UnityEngine;
using System.Collections;

public class Ramen : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Ramen");
    }
}