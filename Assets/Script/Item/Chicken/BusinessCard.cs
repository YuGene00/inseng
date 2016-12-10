using UnityEngine;
using System.Collections;

public class BusinessCard : DisposableItem {

    protected override void EatenByPlayer() {
        Debug.Log("BusinessCard");
    }
}