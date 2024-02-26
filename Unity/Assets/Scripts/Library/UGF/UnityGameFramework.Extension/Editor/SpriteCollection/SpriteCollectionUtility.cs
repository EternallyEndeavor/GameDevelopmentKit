using UnityEditor;

namespace UnityGameFramework.Extension
{
    public static class SpriteCollectionUtility
    {
        public static void RefreshSpriteCollection()
        {
            string[] guids = AssetDatabase.FindAssets("t:SpriteCollection");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SpriteCollection collection = AssetDatabase.LoadAssetAtPath<SpriteCollection>(path);
                collection.Pack();
            }
        }
    }
}