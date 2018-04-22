using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamenSlot : MonoBehaviour {

	public Image image;

	public void UpdateRender(Sprite imagen){
		image.sprite = imagen;
	}
}
