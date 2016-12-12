using UnityEngine;
using System.Collections;

public class AngryCustomer : DisposableItem {

    protected override void EatenByPlayer() {
        SetScore(100);
    }
}