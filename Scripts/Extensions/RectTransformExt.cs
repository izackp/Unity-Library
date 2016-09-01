using UnityEngine;
using System.Collections;

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
		RectTransform parent = trans.parent as RectTransform;

		float anchorPos = (parent.Height() * (1 - trans.anchorMin.y));
		float result = anchorPos + trans.offsetMin.y;

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

		float anchorPos = (parent.Height() * (1 - trans.anchorMin.y));
		return (trans.offsetMin.y - anchorPos) * -1;
		
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
		//RectTransform parent = trans.parent as RectTransform;
		Vector2 offsetMin = trans.offsetMin;
		Vector2 offsetMax = trans.offsetMax;
		//float anchorMin = (parent.Height() * (1 - trans.anchorMin.y));
		float diff = trans.Height() - Height;
		offsetMin.y += diff;

		trans.offsetMin = offsetMin;
	}

    public static void SetWidth(this RectTransform trans, float Width)
    {
        //Vector2 offsetMin = trans.offsetMin;
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

	public static void SetRightPadding(this RectTransform trans, float padding) {
//		RectTransform parent = trans.parent as RectTransform;
//
//		float anchorMaxXPosInverse = (parent.Width() * (1 - trans.anchorMax.x));
//		return (trans.offsetMax.x - anchorMaxXPosInverse) * -1;
//		
		//or return parent.Width() - RightSide();
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
