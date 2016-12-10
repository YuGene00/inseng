using UnityEngine;
using System.Collections;

public class Alcohol : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("Alcohol");
    }
}