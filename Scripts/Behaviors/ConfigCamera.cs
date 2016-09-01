using UnityEngine;

public class ConfigCamera : MonoBehaviour {
	
	public float fZoom = 0;
	float pixelsPerUnit = 1.0f;
	public float ScreenHeight;
	public float ScreenWidth;
    Rect bounds;
	
	void LateUpdate () {
		//pixel perfect = (Screen.height * 0.5f) / 100
		ScreenHeight = Screen.height;
		ScreenWidth = Screen.width;
        

		fZoom += (Input.GetAxis("Mouse ScrollWheel") * 5.0f);

        if (fZoom < MinZoom())
            fZoom = MinZoom();
		
		float s_baseOrthographicSize = (Screen.height * 0.5f - fZoom) / pixelsPerUnit;
		s_baseOrthographicSize = s_baseOrthographicSize - (s_baseOrthographicSize % 2);
		Camera.main.orthographicSize = s_baseOrthographicSize;

        var vertExtent = s_baseOrthographicSize;
        var horzExtent = vertExtent * ScreenWidth / ScreenHeight;

        // Calculations assume map is position at the origin
        float minX = bounds.x + horzExtent;
        float maxX = bounds.xMax - horzExtent;
        float minY = bounds.yMax + vertExtent;
        float maxY = bounds.y - vertExtent;

        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }

    public float MaxCamSize()
    {//4:3 ratio is 1.33, reverse is .75
        //H:3 W:4 - MW:2
        //maxVert = 1.5
        //maxVert2 = 0.75
        float screenRatio = ScreenWidth / ScreenHeight;
        float maxVert = Mathf.Abs(bounds.height) / 2;
        float maxVert2 = bounds.width / screenRatio / 2;
        return (maxVert < maxVert2) ? maxVert : maxVert2;
    }

    public float MinZoom()
    {
        float maxCamSize = MaxCamSize();
        return Screen.height * 0.5f - maxCamSize;
    }

	public void OffsetZoom (float magnitude) {
		fZoom += magnitude;
	}

    public void SetBounds(Rect bounds)
    {
        this.bounds = bounds;
    }
}