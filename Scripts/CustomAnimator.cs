using UnityEngine;
using System.Collections.Generic;

public class KeyFrame<T> {
	public T Value;
	public float Time;
}

public class CustomSpriteAnimationClip {
	public float Length;
	public string Name;
	public KeyFrame<Sprite>[] KeyFrames;

	protected KeyFrame<Sprite> FrameForTime(float time) {
		if (time > Length)
			time -= Length;

		KeyFrame<Sprite> currentFrame = null;
		foreach (KeyFrame<Sprite> frame in KeyFrames)
		{
			if (frame.Time > time)
				return currentFrame;

			currentFrame = frame;
		}

		return currentFrame;
	}

	public void ApplyFrame(float time, SpriteRenderer renderer) {
		
		KeyFrame<Sprite> frame = FrameForTime(time);
		if (renderer.sprite == frame.Value)
			return;
		//Debug.Log("Changing keyframe to: " + frame.Value.name);
		renderer.sprite = frame.Value;
	}
}

public class CustomAnimator : MonoBehaviour {

	public float Speed;
	public SpriteRenderer SprRenderer;
	public bool ShouldLoop;
	public bool Hidden;

	public float CurrentPlayTime;
	public CustomSpriteAnimationClip CurrentClip;

	static GameObject animatorPrefab;
	public static CustomAnimator Build() {
		if (animatorPrefab == null)
			animatorPrefab = Resources.Load<GameObject>("CustomAnimator");
		
		GameObject newAnimatorObj = Instantiate(animatorPrefab) as GameObject;
		CustomAnimator newAnimator = newAnimatorObj.GetComponent<CustomAnimator>();
		
		return newAnimator;
	}

	void Update() {
		float deltaTime = Time.deltaTime;
		CurrentPlayTime += (deltaTime * Speed);

		if (CurrentClip == null)
			return;

		while (CurrentPlayTime > CurrentClip.Length)
			CurrentPlayTime -= CurrentClip.Length;

		if (Hidden) {
			SprRenderer.sprite = null;
			return;
		}

		CurrentClip.ApplyFrame(CurrentPlayTime, SprRenderer);
	}
	
	public void PlayAnimation(CustomSpriteAnimationClip newClip, bool shouldReset) {
		if (CurrentClip != newClip) {
			CurrentPlayTime = 0.0f;
			CurrentClip = newClip;
		}

		if (shouldReset)
			CurrentPlayTime = 0.0f;
	}
}
