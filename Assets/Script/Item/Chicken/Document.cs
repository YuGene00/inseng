using UnityEngine;
using System.Collections;

public class Document : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Document");
    }
}