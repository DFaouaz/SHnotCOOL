using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InputConfiguration : StandaloneInputModule {

	bool mouseActive = false;
	bool selected = false;
	public float timeToDisableMouse;
	float time = -0.5f;


	void Update(){
		if (!Cursor.visible && Input.GetAxis ("Mouse X") != 0)
			VisibleMouse ();
		else if (Cursor.visible && time > -1f) {
			time -= Time.deltaTime;
			if (time <= 0f) {
				InvisibleMouse ();
			}
		}
	}

	void VisibleMouse(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		time = timeToDisableMouse;
		mouseActive = true;
	}
	void InvisibleMouse(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		mouseActive = false;
		time = -1f;
		SelectFirstFoundButton ();
	}

	public bool GetMouseState {
		get{return mouseActive;}
	}

	public void SwitchMouse(){
		mouseActive = !mouseActive;
	}

	public override void Process(){
		bool usedEvent = SendUpdateEventToSelectedObject ();
		if (eventSystem.sendNavigationEvents && !mouseActive) {
			if (!usedEvent)
				usedEvent |= SendMoveEventToSelectedObject ();
			if (!usedEvent)
				SendSubmitEventToSelectedObject ();
		}
		else if (mouseActive) {
			ProcessMouseEvent ();
			DeselectButton ();
			selected = false;
		} else if (!selected) {
			DeselectButton ();
			SelectFirstFoundButton ();
			selected = true;
		}
	}


	public static void SelectFirstFoundButton(){
		bool selected = false;
		int indice = 0;
		Selectable[] aux = Button.allSelectables.ToArray ();
		if (indice < aux.Length) {
			while (!selected) {
				if (aux [indice].interactable) {
					aux [indice].Select ();
					selected = true;
				} else
					indice++;
			}
		}
		if (!selected)
			DeselectButton ();
	}

	public static void SelectFirstSelected(){
		if(EventSystem.current.firstSelectedGameObject != null)
			EventSystem.current.SetSelectedGameObject (EventSystem.current.firstSelectedGameObject);
	}

	public static void DeselectButton(){
		EventSystem.current.SetSelectedGameObject (null);
	}
}
