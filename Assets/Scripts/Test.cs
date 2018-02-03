using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public Spawner spawner;
	public int[] numNodes;
	public int batchSize;
	public int epochs;
	[Range(0, 1)]
	public float learningRate;

	NeuralNetwork net;
	Matrix trainingData;
	char[] trainingLabels;
	Matrix testData;
	char[] testLabels;


	// Use this for initialization
	void Start () {
		net = new NeuralNetwork(numNodes);

		spawner.Spawn(numNodes[numNodes.Length - 1]);

		StartCoroutine("ReadData");
	}

	IEnumerator ReadData() {
		trainingData = DataLoader.LoadTrainData();
		trainingLabels = DataLoader.LoadTrainLabels();
		Debug.Log("Training data loaded");
		testData = DataLoader.LoadTestData();
		testLabels = DataLoader.LoadTestLabels();
		Debug.Log("Testing data loaded");

		StartCoroutine("TrainNetwork");
		return null;
	}

	IEnumerator TrainNetwork() {
		net.Train(trainingData, trainingLabels, epochs, batchSize, learningRate);
		Debug.Log("Network trained");

		StartCoroutine("TestNetwork");
		return null;
	}

	IEnumerator TestNetwork() {
		int numCorrect = 0;
		for(int i = 0; i < 10000; i++) {
			if((i % 100) == 0) {
				Debug.Log(i);
			}

			Matrix nextImage = new Matrix(784, 1);
			for(int j = 0; j < 784; j++) {
				nextImage[j, 0] = testData[j, i];
			}

			Matrix prediction = net.Predict(nextImage);

			float max = -1;
			char choice = (char)10;
			for(int j = 0; j < 10; j++) {
				if(prediction[j, 0] > max) {
					max = prediction[j, 0];
					choice = (char)j;
				}
			}
			if(choice == testLabels[i]) {
				numCorrect++;
			}
			spawner.UpdateNodes(prediction);
			yield return null;
		}
		Debug.Log(numCorrect);
	}

}
