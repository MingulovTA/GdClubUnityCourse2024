using UnityEngine;

namespace Arpa_common.General.Extentions
{
	public static class StringXts
	{
		public static void LogEditor(this string value)
		{
#if UNITY_EDITOR
			Debug.Log(value);
#endif
		}

		public static void LogEditor(this string value, Color color)
		{
#if UNITY_EDITOR
			Debug.LogFormat("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), value);
#endif
		}

		public static string Color(this string value, Color color)
		{
			return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), value);
		}
	}
}
