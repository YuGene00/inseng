using UnityEngine;
using System.Collections;

public class Toiletries : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("toiletries");
    }
}