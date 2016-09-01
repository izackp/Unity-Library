using UnityEngine;

public static class BoundsExt {

	public static bool DoesEncapsulate(this Bounds bounds, Bounds otherBounds)
	{
		Vector3 corner1 = otherBounds.center - otherBounds.extents;
		Vector3 corner2 = otherBounds.center + otherBounds.extents;
		return bounds.DoesEncapsulate(corner1, corner2);
	}

	public static bool DoesEncapsulate(this Bounds bounds, Vector3 corner1, Vector3 corner2)
	{
		if (!bounds.Contains (corner1))
			return false;

		return bounds.Contains(corner2);
	}
}

public static class RectExt
{

    public static bool DoesEncapsulate(this Rect bounds, Rect otherBounds)
    {
        Vector2 corner1 = new Vector2(otherBounds.x, otherBounds.y);
        Vector2 corner2 = new Vector2(otherBounds.xMax, otherBounds.yMax);
        return bounds.DoesEncapsulate(corner1, corner2);
    }

    public static bool DoesEncapsulate(this Rect bounds, Vector2 corner1, Vector2 corner2)
    {
        if (!bounds.Contains(corner1))
            return false;

        return bounds.Contains(corner2);
    }
}