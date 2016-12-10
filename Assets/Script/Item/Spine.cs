using UnityEngine;
using System.Collections;

public class Spine : DisposableItem {

    protected override void EatenByPlayer() {
        Player.instance.DamagedLife(1);
    }
}