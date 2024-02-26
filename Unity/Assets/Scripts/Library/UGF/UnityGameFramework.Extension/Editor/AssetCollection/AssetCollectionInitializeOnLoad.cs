using UnityEditor;

namespace UnityGameFramework.Extension
{
    [InitializeOnLoad]
    public static class AssetCollectionInitializeOnLoad
    {
        static AssetCollectionInitializeOnLoad()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private static void PlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.ExitingEditMode)
            {
                AssetCollectionUtility.RefreshAssetCollection();
            }
        }
    }
}