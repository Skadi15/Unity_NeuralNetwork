using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public float activation;

	Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		rend.material.SetColor("_Color", Color.Lerp(Color.black, Color.white, activation));
	}

}
