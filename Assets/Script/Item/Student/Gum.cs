using UnityEngine;
using System.Collections;

public class Gum : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Gum");
    }
}