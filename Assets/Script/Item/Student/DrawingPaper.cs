using UnityEngine;
using System.Collections;

public class DrawingPaper : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Drawing Paper");
    }
}