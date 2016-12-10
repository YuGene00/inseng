using UnityEngine;
using System.Collections;

public class BankBook : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("BankBook");
    }
}