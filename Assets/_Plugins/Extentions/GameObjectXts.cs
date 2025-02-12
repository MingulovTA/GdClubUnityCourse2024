using UnityEngine;

namespace Arpa_common.General.Extentions
{
    public static class GameObjectXts
    {
        public static void SetLayerRecursively(this GameObject gameObject, string layerName)
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer(layerName));
        }
        public static void SetLayerRecursively(this GameObject gameObject, int layerId)
        {
            gameObject.layer = layerId;
            foreach(Transform child in gameObject.transform)
            {
                child.gameObject.SetLayerRecursively(layerId);
            }
        }
    }
}
