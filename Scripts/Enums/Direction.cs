using UnityEngine;

public enum Direction {
	None,
	Up,
	Down,
	Left,
	Right,
	Forward,
	Backward
}

public static class DirectionExtension
{
	public static Direction Opposite(this Direction direction)
	{
		switch (direction) {
		case Direction.Up:
			return Direction.Down;
			
		case Direction.Down:
			return Direction.Up;
			
		case Direction.Left:
			return Direction.Right;
			
		case Direction.Right:
			return Direction.Left;

		case Direction.Forward:
			return Direction.Backward;

		case Direction.Backward:
			return Direction.Forward;

		}

		return Direction.None;
	}

	public static Direction Random2DDirection()
	{
		int random = Random.Range(0, 5);
		switch (random) {
		case 0:
			return Direction.Down;
			
		case 1:
			return Direction.Up;
			
		case 2:
			return Direction.Right;
			
		case 3:
			return Direction.Left;
			
		}
		
		return Direction.Up;
	}

	public static Vector3 NormalOffset(this Direction direction)
	{
		switch (direction) {
		case Direction.Up:
			return new Vector3(0, -1, 0);
			
		case Direction.Down:
			return new Vector3(0, 1, 0);
			
		case Direction.Left:
			return new Vector3(-1, 0, 0);
			
		case Direction.Right:
			return new Vector3(1, 0, 0);

		case Direction.Forward:
			return new Vector3(0, 0, 1);

		case Direction.Backward:
			return new Vector3(0, 0, -1);
			
		}
		
		return Vector2.zero;
	}

	public static string ToString(this Direction direction)
	{
		switch (direction) {
		case Direction.Up:
			return "Up";
			
		case Direction.Down:
			return "Down";
			
		case Direction.Left:
			return "Left";
			
		case Direction.Right:
			return "Right";
			
		case Direction.Forward:
			return "Forward";
			
		case Direction.Backward:
			return "Backward";
		}
		
		return null;
	}

	public static Direction DirectionFromAngle(int angle) {
		int iAngle = angle + 45;
		iAngle = iAngle / 90;

		while (iAngle < 0)
			iAngle += 4;
		while (iAngle > 3)
			iAngle -= 4;

		switch (iAngle) {
		case 0:
			return Direction.Up;
			
		case 1:
			return Direction.Right;
			
		case 2:
			return Direction.Down;
			
		case 3:
			return Direction.Left;
			
		}

		return Direction.None;


	}
}