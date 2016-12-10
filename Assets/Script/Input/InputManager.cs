using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    //inspector
    public Player player;

    //variable
    Mouse mouse;
    Vector2 playerOriginPos;

    void Awake() {
        mouse = new Mouse();
    }

    void Start() {
        StartCoroutine("BackKey");
        StartCoroutine("InputMouse");
    }

    IEnumerator BackKey() {
        while(true) {
            yield return new WaitUntil(() => (Input.GetKey(KeyCode.Escape)));
            Application.Quit();
        }
    }

    IEnumerator InputMouse() {
        while (true) {
            mouse.UpdateClicking();
            switch(mouse.GetClickState()) {
                case Mouse.ClickState.DOWN:
                    playerOriginPos = player.GetPosition();
                    break;
                case Mouse.ClickState.DRAG:
                    Vector2 distance = mouse.CalculateMouseDistance();
                    MoveCharacterWithMouseDistance(distance);
                    break;
            }
            yield return null;
        }
    }

    void MoveCharacterWithMouseDistance(Vector2 distance) {
        player.Move(playerOriginPos + distance);
    }
}
