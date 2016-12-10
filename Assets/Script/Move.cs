using UnityEngine;
using System.Collections;

public class Move {

    //variable
    Vector2 leftBottom, rightTop;

    public void SetMovableArea(Vector2 leftBottom = default(Vector2), Vector2 rightTop = default(Vector2)) {
        this.leftBottom = leftBottom;
        this.rightTop = rightTop;
    }

    public bool HasMovableArea() {
        return leftBottom != rightTop;
    }

	public Transform MoveTransformTo(Transform moveTarget, Vector2 dest) {
        moveTarget.position = FixPositionWithMovableArea(dest);
        return moveTarget;
    }

    Vector2 FixPositionWithMovableArea(Vector2 origin) {
        if(!HasMovableArea()) {
            return origin;
        }

        Vector2 result = origin;
        if(origin.x < leftBottom.x) {
            result.x = leftBottom.x;
        }
        if(origin.y < leftBottom.y) {
            result.y = leftBottom.y;
        }
        if(origin.x > rightTop.x) {
            result.x = rightTop.x;
        }
        if(origin.y > rightTop.y) {
            result.y = rightTop.y;
        }
        return result;
    }
}
