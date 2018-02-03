using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix {

	private float[,] mat;
	private int rows, cols;

	public int Rows { get { return rows; } }
	public int Cols { get { return cols; } }

	public float this[int x, int y] {
		get { return mat[x, y]; }
		set { mat[x, y] = value; }
	}

	public Matrix(int x, int y) {
		mat = new float[x, y];
		rows = x;
		cols = y;
	}

	public Matrix(int x, int y, float[,] input) {
		mat = new float[x, y];
		rows = x;
		cols = y;

		for(int i = 0; i < x; i++) {
			for(int j = 0; j < y; j++) {
				mat[i, j] = input[i, j];
			}
		}
	}

	static public Matrix Dot(Matrix A, Matrix B) {
		if(A.Cols != B.Rows) {
			Debug.Log(A.Rows);
			Debug.Log(A.Cols);
			Debug.Log(B.Rows);
			Debug.Log(B.Cols);
			throw new UnityException("Inconsistent matrix sizes!");
		}
		Matrix output = new Matrix(A.Rows, B.Cols);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < B.Cols; j++) {
				float sum = 0;
				for(int k = 0; k < A.Cols; k++) {
					sum += A[i, k] * B[k, j];
				}
				output[i, j] = sum;
			}
		}
		return output;
	}

	static public float Sum(Matrix A) {
		float sum = 0;
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				sum += A[i, j];
			}
		}
		return sum;
	}

	static public Matrix Subtract(Matrix A, Matrix B) {
		Matrix output = new Matrix(A.Rows, A.Cols);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				output[i, j] = A[i, j] - B[i, j];
			}
		}
		return output;
	}

	static public Matrix Transpose(Matrix A) {
		Matrix output = new Matrix(A.Cols, A.Rows);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				output[j, i] = A[i, j];
			}
		}
		return output;
	}

	static public Matrix operator* (Matrix A, float scalar) {
		Matrix output = new Matrix(A.Rows, A.Cols);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				output[i, j] = A[i, j] * scalar;
			}
		}
		return output;
	}

	static public Matrix operator* (Matrix A, Matrix B) {
		Matrix output = new Matrix(A.Rows, A.Cols);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				output[i, j] = A[i, j] * B[i, j];
			}
		}
		return output;
	}

	static public Matrix operator+ (Matrix A, Matrix B) {
		Matrix output = new Matrix(A.Rows, A.Cols);
		for(int i = 0; i < A.Rows; i++) {
			for(int j = 0; j < A.Cols; j++) {
				output[i, j] = A[i, j] + B[i, j];
			}
		}
		return output;
	}

}
