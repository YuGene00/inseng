using UnityEngine;
using System.Collections;

public class APlus : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("APlus");
    }
}