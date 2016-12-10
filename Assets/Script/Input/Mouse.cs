using UnityEngine;
using System.Collections;

public class Mouse {

    //Enum
    public enum ClickState {
        NONE, DOWN, DRAG, UP
    }

    //variable
    Vector2 clickPos;
    ClickState state = ClickState.NONE;

    public void UpdateClicking() {
        if (Input.GetMouseButtonDown(0)) {
            clickPos = Input.mousePosition;
            state = ClickState.DOWN;
        } else if (Input.GetMouseButtonUp(0)) {
            state = ClickState.UP;
        } else if (state == ClickState.DOWN) {
            state = ClickState.DRAG;
        } else if(state == ClickState.UP) {
            state = ClickState.NONE;
        }
    }

    public ClickState GetClickState() {
        return state;
    }

    public Vector2 CalculateMouseDistance() {
        return (Vector2)Input.mousePosition - clickPos;
    }
}
