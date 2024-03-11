using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public List<Texture2D> cursors = new List<Texture2D>();
    /*
     * 1 - нормальний стан
     * 2 - швидкий клік
     * 3 - затримка натиску
     */

    private CursorControls cursorControlScript;

    private void Awake()
    {
        cursorControlScript = new CursorControls();

        ChangeCursor(cursors[0]);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        cursorControlScript.Enable();
    }

    private void OnDisable()
    {
        cursorControlScript.Disable();
    }

    private void Start()
    {
        cursorControlScript.Mouse.Click.started += _ => StartedClick();
        cursorControlScript.Mouse.Click.performed += _ => NormalStateCursor();

        cursorControlScript.Mouse.Drag.started += _ => StartedDrag();
        cursorControlScript.Mouse.Drag.performed += _ => NormalStateCursor();

    }

    private void StartedClick()
    {
        ChangeCursor(cursors[1]);
    }

    private void StartedDrag()
    {
        ChangeCursor(cursors[2]);
    }


    private void NormalStateCursor()
    {
        ChangeCursor(cursors[0]);
    }


    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 1.5f, cursorType.height / 1.5f);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }
}
