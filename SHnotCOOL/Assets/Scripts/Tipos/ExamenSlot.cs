using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamenSlot : MonoBehaviour {

	public Image image;

	public void UpdateRender(Sprite imagen){
		if (imagen != null) {
			image.color = new Color (1f, 1f, 1f, 1f);
			image.sprite = imagen;
		} else
			image.color = new Color ();;
	}
}
