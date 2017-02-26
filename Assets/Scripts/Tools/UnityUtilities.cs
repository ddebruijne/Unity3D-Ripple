using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityUtilities {

    /// <summary>
    /// Start a timed routine that calls the action every time it progresses. Returns a value between 0 and 1.
    /// </summary>
    /// <param name="routineObject">The object the routine runs on</param>
    /// <param name="duration">Duration in seconds</param>
    /// <param name="onProgress">Action to be called everytime progress is made</param>
    public static Coroutine StartTimedRoutine(MonoBehaviour routineObject, float duration, Action<float> onProgress) {
        return routineObject.StartCoroutine(TimedRoutine(duration, onProgress));
    }

    private static IEnumerator TimedRoutine(float duration, Action<float> onProgress) {
        float startTime = Time.time;

        while (true) {
            float progress = UnityUtilities.GetNormalizedTime01(startTime, duration, Time.time);
            onProgress(progress);

            //Break when done
            if (progress >= 1) break;

            yield return null;
        }
    }
    
    /// <summary>
    /// Gets the normalized time for a set duration. Useful for lerping.
    /// </summary>
    /// <param name="startTime">Start time in seconds</param>
    /// <param name="duration">Duration in seconds</param>
    /// <param name="currentTime">Current time in seconds</param>
    /// <returns>A value between 0 and 1</returns>
    public static float GetNormalizedTime01(float startTime, float duration, float currentTime) {
        return Mathf.Clamp01((currentTime - startTime) / duration);
    }

    public static Coroutine ExecuteAfterDelay(MonoBehaviour routineObject, float delay, Action action) {
        return routineObject.StartCoroutine(DelayedExecute(delay, action));
    }

    private static IEnumerator DelayedExecute(float delay, Action action) {
        yield return new WaitForSeconds(delay);
        action();
    }
}
