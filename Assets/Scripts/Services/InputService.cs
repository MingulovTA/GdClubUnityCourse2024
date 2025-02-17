//Todo: remove this static shit

using UnityEngine;

public static class InputService
{
    public static float GetAxis(string axisId) => Input.GetAxis(axisId);

    public static bool GetKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);

    public static bool GetKey(KeyCode keyCode) => Input.GetKey(keyCode);
}
