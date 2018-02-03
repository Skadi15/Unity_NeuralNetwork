using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Node node;
	public float spacing;

	Node[] nodes;
	Node[,] imageNodes;

	public void Spawn(int numNodes) {
		nodes = new Node[numNodes];
		float firstPosition = -((float)numNodes - 1) / 2;
		for(int i = 0; i < numNodes; i++) {
			Vector3 position = new Vector3(firstPosition + (i * spacing), -8.4f, 0);
			Node newNode = Instantiate(node, position, Quaternion.identity);
			nodes[i] = newNode;
		}

		imageNodes = new Node[28, 28];
		for(int i = 0; i < 28; i++) {
			for(int j = 0; j < 28; j++) {
				Vector3 position = new Vector3((j * 0.6f) - 8.4f, (i * -0.6f) + 9.4f, 0);
				imageNodes[i, j] = Instantiate(node, position, Quaternion.identity);
			}
		}
	}

	public void UpdateNodes(Matrix activations) {
		for(int i = 0; i < activations.Rows; i++) {
			nodes[i].activation = activations[i, 0];
		}
	}

	public void DrawNumber(Matrix image) {
		for(int i = 0; i < 28; i++) {
			for(int j = 0; j < 28; j++) {
				imageNodes[i, j].activation = image[i * 28 + j, 0];
			}
		}
	}
}
