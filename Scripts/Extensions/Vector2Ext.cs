using UnityEngine;

public static class Vector2Ext {

	public static Direction ToDirection(this Vector2 velocity)
	{
		if (velocity == Vector2.zero)
			return Direction.None;
		
		float horPower = Mathf.Abs(velocity.x);
		float vertPower = Mathf.Abs(velocity.y);
		if (horPower > vertPower)
		{
			if (velocity.x > 0)
				return Direction.Right;
			
			return Direction.Left;
		}
		
		if (velocity.y > 0)
			return Direction.Down;
		
		return Direction.Up;
	}

	public static Direction To2DDirection(this Vector3 velocity)
	{
		if (velocity.x == 0.0f && velocity.y == 0.0f)
			return Direction.None;
		
		float horPower = Mathf.Abs(velocity.x);
		float vertPower = Mathf.Abs(velocity.y);
		if (horPower > vertPower)
		{
			if (velocity.x > 0)
				return Direction.Right;
			
			return Direction.Left;
		}
		
		if (velocity.y > 0)
			return Direction.Down;
		
		return Direction.Up;
	}

	public static float Angle(this Vector2 p_vector2)
	{
		if (p_vector2.x < 0)
		{
			return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
		}
		
		return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
	}
}
