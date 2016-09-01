using UnityEngine;

public static class Texture2DExt {

	public static Rect Frame(this Texture texture) {
		return new Rect(0, 0, texture.width, texture.height);
	}

	public static Texture2D FlipTextureHorizontally(this Texture2D original) {
		Texture2D flipped = new Texture2D(original.width, original.height);
		
		int width = original.width;
		int height = original.height;
		
		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				Color color = original.GetPixel(i, j);
				flipped.SetPixel(width - i - 1, j, color);
			}
		}
		flipped.filterMode = original.filterMode;
		flipped.Apply();
		
		return flipped;
	}

	public static Texture2D FlipTextureVerticially(this Texture2D original) {
		Texture2D flipped = new Texture2D(original.width, original.height);
		
		int width = original.width;
		int height = original.height;
		
		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				Color color = original.GetPixel(i, j);
				flipped.SetPixel(i, height - i - 1, color);
			}
		}
		flipped.filterMode = original.filterMode;
		flipped.Apply();
		
		return flipped;
	}

	public static Texture2D FlipTextureVerticiallyAndHorizontally(this Texture2D original) {
		Texture2D flipped = new Texture2D(original.width, original.height);
		
		int width = original.width;
		int height = original.height;
		
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Color color = original.GetPixel(i, j);
				flipped.SetPixel(width - i - 1, height - i - 1, color);
			}
		}
		flipped.filterMode = original.filterMode;
		flipped.Apply();
		
		return flipped;
	}
}
