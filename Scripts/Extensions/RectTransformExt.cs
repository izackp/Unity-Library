using UnityEngine;
using System.Collections;

public enum RectAnchor {
    Left,
    Right,
    XCenter,
    XStretch,
    Top,
    Bottom,
    YCenter,
    YStretch
}

public static class RectAnchorExt {
    public static float Min(this RectAnchor anchor) {
        switch (anchor) {
            case RectAnchor.Left:
            case RectAnchor.XStretch:
            case RectAnchor.Bottom:
            case RectAnchor.YStretch:
                return 0.0f;

            case RectAnchor.Right:
            case RectAnchor.Top:
                return 1.0f;

            case RectAnchor.XCenter:
            case RectAnchor.YCenter:
                return 0.5f;
        }
        return 0.0f;
    }

    public static float Max(this RectAnchor anchor) {
        switch (anchor) {
            case RectAnchor.Left:
            case RectAnchor.Bottom:
                return 0.0f;

            case RectAnchor.XStretch:
            case RectAnchor.YStretch:
            case RectAnchor.Right:
            case RectAnchor.Top:
                return 1.0f;

            case RectAnchor.XCenter:
            case RectAnchor.YCenter:
                return 0.5f;
        }
        return 0.0f;
    }

    public static bool IsX(this RectAnchor anchor) {
        switch (anchor) {
            case RectAnchor.Left:
            case RectAnchor.Right:
            case RectAnchor.XStretch:
            case RectAnchor.XCenter:
                return true;
        }
        return false;
    }
}

public static class RectTransformExt {

    

	public static float X(this RectTransform trans)
	{
		RectTransform parent = trans.parent as RectTransform;

		float anchorMinXPos = parent.Width() * trans.anchorMin.x;
		return trans.offsetMin.x + anchorMinXPos;
	}

	public static float Y(this RectTransform trans)
	{
		RectTransform parent = trans.parent as RectTransform;

		float anchorMaxYPos = (parent.Height() * (1 - trans.anchorMax.y));
		return trans.offsetMax.y - anchorMaxYPos;
	}

	public static float RightSide(this RectTransform trans) {
		RectTransform parent = trans.parent as RectTransform;

		float anchorMaxXPos = parent.Width() * trans.anchorMax.x;
		return trans.offsetMax.x + anchorMaxXPos;

		//or (trans.Width() + trans.X());
	}

	public static float BottomSide(this RectTransform trans) {

        /*
		float anchorMaxYPos = (parent.Height() * (1 - trans.anchorMax.y));
		return trans.offsetMax.y - anchorMaxYPos;
        */
		RectTransform parent = trans.parent as RectTransform;
        //20k * (0.5)
        //10k + - 1000
        //9k
        //20k - 9k = 11k

        //442 * (-100*-1)
        /*
        float parentHeight = parent.Height();
        float anchorPos = parentHeight * trans.anchorMin.y;
		float result = parentHeight - (anchorPos + trans.offsetMin.y);*/

        float anchorMinYPos = (parent.Height() * (1 - trans.anchorMin.y));
        float result = trans.offsetMin.y - anchorMinYPos;

        return result;
		//or (trans.Height() + trans.Y());
	}

	public static float RightPadding(this RectTransform trans) {
		RectTransform parent = trans.parent as RectTransform;

		float anchorMaxXPosInverse = (parent.Width() * (1 - trans.anchorMax.x));
		return (trans.offsetMax.x - anchorMaxXPosInverse) * -1;

		//or return parent.Width() - RightSide();
	}

	public static float BottomPadding(this RectTransform trans) {
		RectTransform parent = trans.parent as RectTransform;

		float anchorPos = (parent.Height() * (trans.anchorMin.y * -1));
		return (trans.offsetMin.y - anchorPos);
		
		//or return parent.Height() - BottomSide();
	}

	public static void SetX(this RectTransform trans, float x)
	{
		RectTransform parent = trans.parent as RectTransform;
		Vector2 offsetMin = trans.offsetMin;
		float anchorMinXPos = parent.Width() * trans.anchorMin.x;
		offsetMin.x = x - anchorMinXPos;

		float diff = offsetMin.x - trans.offsetMin.x;
		trans.offsetMax += new Vector2(diff, 0.0f);

		trans.offsetMin = offsetMin;
	}
	
	public static void SetY(this RectTransform trans, float y)
	{
		RectTransform parent = trans.parent as RectTransform;
		Vector2 offsetMax = trans.offsetMax;
		float anchorMaxYPos = (parent.Height() * (1 - trans.anchorMax.y));
		offsetMax.y = y + anchorMaxYPos;

		float diff = offsetMax.y - trans.offsetMax.y;
		trans.offsetMin += new Vector2(0.0f, diff);

		trans.offsetMax = offsetMax;
	}

	public static void SetHeight(this RectTransform trans, float Height)
	{
        
		Vector2 offsetMin = trans.offsetMin;
		Vector2 offsetMax = trans.offsetMax;
		float diff = trans.Height() - Height;
		offsetMin.y += diff;

		trans.offsetMin = offsetMin;
	}

    public static void SetHeightBottomAnchored(this RectTransform trans, float Height) {

        trans.sizeDelta = new Vector2(trans.sizeDelta.x, Height);
    }

    public static void SetWidth(this RectTransform trans, float Width)
    {
        Vector2 offsetMax = trans.offsetMax;
        float diff = trans.Width() - Width;
        offsetMax.x -= diff;

        trans.offsetMax = offsetMax;
    }

    public static void FitToParent(this RectTransform trans) {
		trans.anchorMin = Vector2.zero;
		trans.anchorMax = Vector2.one;
		trans.offsetMin = Vector2.zero;
		trans.offsetMax = Vector2.one;
	}

	public static Vector2 Size(this RectTransform trans)
	{
		return trans.rect.size;
	}
	
	public static float Width(this RectTransform trans)
	{
		return trans.rect.width;
	}
	
	public static float Height(this RectTransform trans)
	{
		return trans.rect.height;
	}

    public static void SetAnchor(this RectTransform trans, RectAnchor anchor) {
        if (anchor.IsX()) {
            trans.anchorMin = new Vector2(anchor.Min(), trans.anchorMin.y);
            trans.anchorMax = new Vector2(anchor.Max(), trans.anchorMax.y);
        }
        else {
            trans.anchorMin = new Vector2(trans.anchorMin.x, anchor.Min());
            trans.anchorMax = new Vector2(trans.anchorMax.x, anchor.Max());
        }
    }

    //Useful logging
    static Vector2 anchorMax;
	static Vector2 lastAnchor;
	static Vector2 anchorMin;
	static Vector2 offsetMin;
	static Vector2 offsetMax;
	static void SomeLogging (this RectTransform addDeckTrans) {

		Vector2 currentAnchor = addDeckTrans.anchoredPosition;
		Vector2 currentAnchorMin = addDeckTrans.anchorMin;
		Vector2 currentAnchorMax = addDeckTrans.anchorMax;
		Vector2 currentOffsetMin = addDeckTrans.offsetMin;
		Vector2 currentOffsetMax = addDeckTrans.offsetMax;
		
		if (lastAnchor != currentAnchor) {
			Log (currentAnchor, "anchor");
			lastAnchor = currentAnchor;
		}
		
		if (anchorMin != currentAnchorMin) {
			Log (currentAnchorMin, "anchorMin");
			anchorMin = currentAnchorMin;
		}
		
		
		if (anchorMax != currentAnchorMax) {
			Log (currentAnchorMax, "anchorMax");
			anchorMax = currentAnchorMax;
		}
		
		if (offsetMax != currentOffsetMax) {
			Log (currentOffsetMax, "OffsetMax");
			offsetMax = currentOffsetMax;
		}
		
		if (offsetMin != currentOffsetMin) {
			Log (currentOffsetMin, "OffsetMin");
			offsetMin = currentOffsetMin;
		}
	}
	
	static void Log(Vector3 vector, string name) {
		Debug.Log(name + ": " + vector.x + ", " + vector.y + ", " + vector.z);
	}
}
