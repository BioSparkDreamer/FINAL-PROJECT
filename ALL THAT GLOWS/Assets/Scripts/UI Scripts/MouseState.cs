using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseState : MonoBehaviour
{
    public CursorLockMode _cursorModeOnStart;

	public void Start()
	{
		Cursor.visible = true;
		Cursor.lockState = _cursorModeOnStart;
	}
}
