using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    //singleton
    public static InputManager instance = null;

    //inspector
    public Player player;

    //variable
    Mouse mouse;
    Vector2 playerOriginPos;
    bool active = true;
    public bool Active {
        get {
            return active;
        }
        set {
            active = value;
        }
    }
    WaitUntil waitForActive;

    void Awake() {
        instance = this;
        InitVariable();
    }

    void InitVariable() {
        mouse = new Mouse();
        waitForActive = new WaitUntil(() => (active));
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
            switch(mouse.GetClickState) {
                case Mouse.ClickState.DOWN:
                    playerOriginPos = player.GetPosition();
                    break;
                case Mouse.ClickState.DRAG:
                    Vector2 distance = mouse.CalculateMouseDistance();
                    MoveCharacterWithMouseDistance(distance);
                    break;
            }
            yield return waitForActive;
        }
    }

    void MoveCharacterWithMouseDistance(Vector2 distance) {
        player.Move(playerOriginPos + distance);
    }
}
