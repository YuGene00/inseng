using UnityEngine;
using System.Collections;

public class Assignment : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Assignment");
    }
}