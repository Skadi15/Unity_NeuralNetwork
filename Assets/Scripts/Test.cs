using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public Spawner spawner;
	public int[] numNodes;

	// Use this for initialization
	void Start () {
		NeuralNetwork net = new NeuralNetwork(numNodes);
		Matrix trainingInputs = Matrix.Transpose(new Matrix(4, 3, new float[,]{{0, 0, 1},{1, 1, 1},{1, 0, 1},{0, 1, 1}}));
		Matrix trainingOutputs = Matrix.Transpose(new Matrix(4, 2, new float[,]{ {0, 0}, {1, 0}, {1, 0}, {0, 0} }));
		Matrix predictData = Matrix.Transpose(new Matrix(1, 3, new float[,]{ { 1, 0, 0 } }));

		spawner.Spawn(numNodes[numNodes.Length - 1]);

		Matrix prediction = net.Predict(predictData);
		Debug.Log(prediction[0,0]);
		net.Train(trainingInputs, trainingOutputs, 1000);
		prediction = net.Predict(predictData);
		spawner.UpdateNodes(new float[]{ prediction[0, 0], prediction[1, 0] });
		Debug.Log(prediction[0,0]);
	}

}
