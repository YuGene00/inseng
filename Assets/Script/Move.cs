using UnityEngine;
using System.Collections;

public class Move {

    //variable
    AreaController areaController;

    public Move() {
        areaController = new AreaController();
    }

    public void SetMovableArea(Vector2 leftBottom, Vector2 rightTop) {
        areaController.SetArea(leftBottom, rightTop);
    }

	public Transform MoveTransToDestInArea(Transform moveTarget, Vector2 dest) {
        moveTarget.position = areaController.FixPositionInArea(dest);
        return moveTarget;
    }

    public Transform MoveTransToDest(Transform moveTarget, Vector2 dest) {
        moveTarget.position = dest;
        return moveTarget;
    }

    public bool IsInArea(Vector2 origin) {
        return areaController.IsInArea(origin);
    }
}
