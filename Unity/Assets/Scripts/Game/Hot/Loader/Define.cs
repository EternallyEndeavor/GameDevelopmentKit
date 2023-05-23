﻿namespace Game.Hot
{
    public static class Define
    {
#if UNITY_EDITOR
        public static bool IsEditor => true;
#else
        public static bool IsEditor => false;
#endif

#if UNITY_HOTFIX
        public static bool EnableHotfix => true;
#else
        public static bool EnableHotfix => false;
#endif

#if UNITY_HOTFIX && UNITY_EDITOR && UNITY_HOTFIX_EDITORRUN
        public static bool IsHotfixEditorRun => true;
#else
        public static bool IsHotfixEditorRun => false;
#endif

#if ENABLE_IL2CPP
        public static bool EnableIL2CPP = true;
#else
        public static bool EnableIL2CPP = false;
#endif
    }
}