using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ArpaSubmodules.ArpaCommon.General.Extentions.Tween
{
    public static class DoTweenExtentions
    {
        public static DG.Tweening.Tween SetAlpha(this SpriteRenderer sprite, float alpha, float time = 0.0f,
            Action completeCallback = null)
        {
            Color newClr = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);

            if (time > 0.001f)
            {
                return DOTween.To(() => sprite.color.a,
                        newAlpha =>
                        {
                            newClr.a = newAlpha;
                            sprite.color = newClr;
                        },
                        alpha, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        completeCallback?.Invoke();
                    });
            }
            newClr.a = alpha;
            sprite.color = newClr;
            return null;
        }

        public static DG.Tweening.Tween SetColor(this SpriteRenderer sprite, Color color, float time = 0.0f,
            Action completeCallback = null)
        {
        
            if (time > 0.001f)
            {
                return DOTween.To(() => sprite.color, newColor => sprite.color = newColor, color, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        if (completeCallback != null)
                        {
                            completeCallback();
                        }
                    });
            }

            sprite.color = color;
            return null;
        }
    
        public static DG.Tweening.Tween SetAlpha(this Image img, float alpha, float time = 0.0f,
            Action completeCallback = null)
        {
            Color newClr = new Color(img.color.r, img.color.g, img.color.b, alpha);

            if (time > 0.001f)
            {
                return DOTween.To(() => img.color.a,
                        newAlpha =>
                        {
                            newClr.a = newAlpha;
                            img.color = newClr;
                        },
                        alpha, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        completeCallback?.Invoke();
                    });
            }
            newClr.a = alpha;
            img.color = newClr;
            return null;
        }
    
        public static DG.Tweening.Tween SetColor(this Image img, Color color, float time = 0.0f,
            Action completeCallback = null)
        {
        
            if (time > 0.001f)
            {
                return DOTween.To(() => img.color, newColor => img.color = newColor, color, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        if (completeCallback != null)
                        {
                            completeCallback();
                        }
                    });
            }

            img.color = color;
            return null;
        }
    
        public static DG.Tweening.Tween SetColor(this Text text, Color color, float time = 0.0f,
            Action completeCallback = null)
        {
        
            if (time > 0.001f)
            {
                return DOTween.To(() => text.color, newColor => text.color = newColor, color, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        if (completeCallback != null)
                        {
                            completeCallback();
                        }
                    });
            }

            text.color = color;
            return null;
        }
        
        public static DG.Tweening.Tween SetAlpha(this Text text, float alpha, float time = 0.0f,
            Action completeCallback = null)
        {
            Color newClr = new Color(text.color.r, text.color.g, text.color.b, alpha);

            if (time > 0.001f)
            {
                return DOTween.To(() => text.color.a,
                        newAlpha =>
                        {
                            newClr.a = newAlpha;
                            text.color = newClr;
                        },
                        alpha, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(delegate
                    {
                        completeCallback?.Invoke();
                    });
            }
            newClr.a = alpha;
            text.color = newClr;
            return null;
        }
    }
}
