using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Side {
	Up, Down, Left, Right, None, All
}

public static class SideMethods {

	public static int DeltaX(this Side s) {
		switch (s) {
			case Side.Left: return -1;
			case Side.Right: return 1;
			default: return 0;
		}
	}

	public static int DeltaY(this Side s) {
		switch (s) {
			case Side.Up: return -1;
			case Side.Down: return 1;
			default: return 0;
		}
	}

	public static Side Opposite(this Side s) {
		switch (s) {
			case Side.Left: return Side.Right;
			case Side.Right: return Side.Left;
		}
		throw new Exception("There is no need for opposite different to left and right");
	}
}