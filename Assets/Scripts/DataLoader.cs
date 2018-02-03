using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class DataLoader {

	public static Matrix LoadTrainData() {
		FileStream dataStream = new FileStream("train-images.idx3-ubyte", FileMode.Open);
		byte[] readBytes = new byte[4];
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int magicNumber = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int numImages = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int rows = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int cols = BitConverter.ToInt32(readBytes, 0);
		Matrix images = new Matrix(rows * cols, numImages);

		for(int i = 0; i < numImages; i++) {
			for(int j = 0; j < rows * cols; j++) {
				images[j, i] = (float)dataStream.ReadByte() / 255.0f;
			}
		}
		return images;
	}

	public static char[] LoadTrainLabels() {
		FileStream dataStream = new FileStream("train-labels.idx1-ubyte", FileMode.Open);
		byte[] readBytes = new byte[4];
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int magicNumber = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int numImages = BitConverter.ToInt32(readBytes, 0);

		char[] labels = new char[numImages];
		for(int i = 0; i < numImages; i++) {
			labels[i] = (char)dataStream.ReadByte();
		}
		return labels;
	}

	public static Matrix LoadTestData() {
		FileStream dataStream = new FileStream("t10k-images.idx3-ubyte", FileMode.Open);
		byte[] readBytes = new byte[4];
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int magicNumber = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int numImages = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int rows = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int cols = BitConverter.ToInt32(readBytes, 0);
		Matrix images = new Matrix(rows * cols, numImages);

		for(int i = 0; i < numImages; i++) {
			for(int j = 0; j < rows * cols; j++) {
				images[j, i] = (float)dataStream.ReadByte() / 255.0f;
			}
		}
		return images;
	}

	public static char[] LoadTestLabels() {
		FileStream dataStream = new FileStream("t10k-labels.idx1-ubyte", FileMode.Open);
		byte[] readBytes = new byte[4];
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int magicNumber = BitConverter.ToInt32(readBytes, 0);
		dataStream.Read(readBytes, 0, sizeof(int));
		Array.Reverse(readBytes);
		int numImages = BitConverter.ToInt32(readBytes, 0);

		char[] labels = new char[numImages];
		for(int i = 0; i < numImages; i++) {
			labels[i] = (char)dataStream.ReadByte();
		}
		return labels;
	}

}
