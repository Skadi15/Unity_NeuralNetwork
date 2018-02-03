using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {
	
	private Matrix[] weights;
	private Matrix[] layerInputs;

	public NeuralNetwork(int[] numNodes) {
		Random.InitState(1);
		weights = new Matrix[numNodes.Length - 1];
		layerInputs = new Matrix[numNodes.Length - 1];
		for(int i = 0; i < (numNodes.Length - 1); i++) {
			weights[i] = new Matrix(numNodes[i + 1], numNodes[i]);
			for(int j = 0; j < numNodes[i + 1]; j++) {
				for(int k = 0; k < numNodes[i]; k++) {
					weights[i][j, k] = Random.Range(-1.0f, 1.0f);
				}
			}
		}
	}

	public Matrix Predict(Matrix inputs) {
		Matrix outputs = inputs;
		for(int i = 0; i < weights.Length; i++) {
			layerInputs[i] = outputs;
			outputs = Sigmoid(Matrix.Dot(weights[i], outputs));
		}
		return outputs;
	}

	public void Train(Matrix trainingInputs, char[] trainingOutputs, int epochs, int batchSize, float learningRate) {
		Matrix output, error, deltas, temp;
		for(int epoch = 0; epoch < epochs; epoch++) {
			for(int batch = 0; batch < (60000 / batchSize); batch++) {
				Matrix batchInputs = new Matrix(28 * 28, batchSize);
				Matrix batchOutputs = new Matrix(10, batchSize);
				for(int image = 0; image < batchSize; image++) {
					for(int pixel = 0; pixel < (28 * 28); pixel++) {
						batchInputs[pixel, image] = trainingInputs[pixel, (batch * batchSize + image)];
					}
					char label = trainingOutputs[batch * batchSize + image];
					for(int i = 0; i < 10; i++) {
						batchOutputs[i, image] = (label == i) ? 1 : 0;
					}
				}
				output = Predict(batchInputs);
				error = Matrix.Subtract(batchOutputs, output);

				for(int j = weights.Length - 1; j >= 0; j--) {
					temp = error * SigmoidDerivative(output);
					deltas = Matrix.Dot(temp, Matrix.Transpose(layerInputs[j]));
					error = Matrix.Dot(Matrix.Transpose(weights[j]), temp);
					weights[j] = weights[j] + deltas * learningRate;
					output = layerInputs[j];
				}
			}
		}
	}

	private Matrix Sigmoid(Matrix input) {
		Matrix output = new Matrix(input.Rows, input.Cols);
		for(int i = 0; i < input.Rows; i++) {
			for (int j = 0; j < input.Cols; j++) {
				output[i, j] = 1 / (1 + Mathf.Exp(-input[i, j]));
			}
		}
		return output;
	}

	private Matrix SigmoidDerivative(Matrix output) {
		Matrix returnVal = new Matrix(output.Rows, output.Cols);
		for(int i = 0; i < output.Rows; i++) {
			for(int j = 0; j < output.Cols; j++) {
				returnVal[i, j] = output[i, j] * (1 - output[i, j]);
			}
		}
		return returnVal;
	}

	private float ReLu(float input) {
		if (input > 0) {
			return input;
		} 
		else {
			return 0;
		}
	}

	private Matrix ReLu(Matrix input) {
		Matrix output = new Matrix(input.Rows, input.Cols);
		for(int i = 0; i < input.Rows; i++) {
			for(int j = 0; j < input.Cols; j++) {
				output[i, j] = ReLu(input[i, j]);
			}
		}
		return output;
	}

	private float ReLuDerivative(float input) {
		if(input > 0) {
			return 1;
		}
		else {
			return 0;
		}
	}

	private Matrix ReLuDerivative(Matrix input) {
		Matrix output = new Matrix(input.Rows, input.Cols);
		for(int i = 0; i < input.Rows; i++) {
			for(int j = 0; j < input.Cols; j++) {
				output[i, j] = ReLuDerivative(input[i, j]);
			}
		}
		return output;
	}

}
