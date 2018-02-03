using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Node node;
	public float spacing;

	Node[] nodes;

	public void Spawn(int numNodes) {
		nodes = new Node[numNodes];
		float firstPosition = -((float)numNodes - 1) / 2;
		for(int i = 0; i < numNodes; i++) {
			Vector3 position = new Vector3(firstPosition + (i * spacing), 1, 0);
			Node newNode = Instantiate(node, position, Quaternion.identity);
			nodes[i] = newNode;
		}
	}

	public void UpdateNodes(Matrix activations) {
		for(int i = 0; i < activations.Rows; i++) {
			nodes[i].activation = activations[i, 0];
		}
	}

}
