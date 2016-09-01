using UnityEngine;
using System.Collections;
using System.Threading;

public static class WWWExt {

	public static WWW Load(string resourcePath) {
		WWW resource = new WWW(resourcePath);
		while (resource.isDone == false)
			Thread.Sleep(1);
		return resource;
	}

	public static string LoadText(string resourcePath) {
		WWW resource = Load(resourcePath);
		return resource.text;
	}

	public static Texture2D LoadTexture(string resourcePath) {
		WWW resource = Load(resourcePath);
		return resource.texture;
	}
}
