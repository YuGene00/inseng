using UnityEngine;
using System.Collections;

public class DisappearMan : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("DisappearMan");
    }
}