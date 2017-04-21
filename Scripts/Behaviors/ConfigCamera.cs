using UnityEngine;

public class ConfigCamera : MonoBehaviour {

    public float Zoom = 1.0f;
    float _pixelsPerUnit = 1.0f; //Set this to whatever you set for your texture
    public float ScreenWidth; //For Debugging
    public float ScreenHeight; //For Debugging
    Rect _bounds;

    void Start() {
        int numTilesWidth = 40;
        int numTilesHeight = 40;
        int tileWidth = 16;
        int tileHeight = 16;
        int mapWidth = numTilesWidth * tileWidth;
        int mapHeight = numTilesHeight * tileHeight;
        SetBounds(new Rect(0, 0, mapWidth, mapHeight * -1));
    }

    void LateUpdate() {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        //Only For Debugging
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;

        Zoom += (Input.GetAxis("Mouse ScrollWheel") * 5.0f);
        float minZoom = MinZoom(screenWidth, screenHeight);
        if (Zoom < minZoom)
            Zoom = minZoom;

        //Determine camera size based on screen height, zoom, and pixels per unit
        float baseOrthographicSize = (Screen.height * 0.5f - Zoom) / _pixelsPerUnit;
        baseOrthographicSize = baseOrthographicSize - (baseOrthographicSize % 2); //We round it down so its divisble by 2 //Ex: 13 - (13%2) = 12
        Camera.main.orthographicSize = baseOrthographicSize;

        float vertExtent = baseOrthographicSize;
        float horzExtent = vertExtent * screenWidth / screenHeight;

        //Establish bounds so the camera doesn't go offscreen
        // Calculations assume map is position at the origin
        float minX = _bounds.x + horzExtent;
        float maxX = _bounds.xMax - horzExtent;
        float minY = _bounds.yMax + vertExtent;
        float maxY = _bounds.y - vertExtent;

        //Keep camera from leaving bounds
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }

    public float MaxCamSize(float screenWidth, float screenHeight) {
        float screenRatio = screenWidth / screenHeight;
        float maxVert = Mathf.Abs(_bounds.height) * 0.5f;
        float maxVert2 = _bounds.width / screenRatio * 0.5f;
        return (maxVert < maxVert2) ? maxVert : maxVert2;
    }

    public float MinZoom(float screenWidth, float screenHeight) {
        float maxCamSize = MaxCamSize(screenWidth, screenHeight);
        return Screen.height * 0.5f - maxCamSize;
    }

    public void OffsetZoom(float magnitude) {
        Zoom += magnitude;
    }

    public void SetBounds(Rect bounds) {
        _bounds = bounds;
    }
}
