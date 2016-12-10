using UnityEngine;
using System.Collections;

public class Superior : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Superior");
    }
}
