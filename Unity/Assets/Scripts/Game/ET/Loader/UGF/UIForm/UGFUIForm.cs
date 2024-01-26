using CodeBind;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = Game.GameEntry;

namespace ET
{
    [ChildOf]
    public sealed class UGFUIForm : Entity, IAwake<int, ETMonoUIForm>, IDestroy
    {
        [BsonIgnore]
        public UIForm uiForm { get; private set; }
        public int uiFormId { get; private set; }
        [BsonIgnore]
        public Transform transform { get; private set; }
        /// <summary>
        /// 界面是否开启
        /// </summary>
        public bool isOpen => this.m_ETMonoUIForm.isOpen;
        [BsonIgnore]
        private ETMonoUIForm m_ETMonoUIForm;
        public ListComponent<EntityRef<UGFUIWidget>> UIWidgets;

        internal void OnAwake(int formId, ETMonoUIForm ugfETUIForm)
        {
            this.uiFormId = formId;
            this.m_ETMonoUIForm = ugfETUIForm;
            this.uiForm = ugfETUIForm.UIForm;
            this.transform = ugfETUIForm.CachedTransform;
            m_UIWidgets = ListComponent<EntityRef<UGFUIWidget>>.Create();
        }

        internal void OnDestroy()
        {
            ETMonoUIForm etMonoUIForm = this.m_ETMonoUIForm;
            this.uiFormId = default;
            this.m_ETMonoUIForm = default;
            this.uiForm = default;
            this.transform = default;
            this.UIWidgets?.Dispose();
            this.UIWidgets = null;
            if (etMonoUIForm != default && etMonoUIForm.isOpen)
            {
                GameEntry.UI.CloseUIForm(etMonoUIForm.UIForm);
            }
        }

        internal void AddUIWidget(UGFUIWidget widget)
        {
            
        }

        internal void RemoveUIWidget(UGFUIWidget widget)
        {
            
        }
    }
    
    [EntitySystemOf(typeof(UGFUIForm))]
    [FriendOf(typeof(UGFUIForm))]
    public static partial class UGFUIFormSystem
    {
        [EntitySystem]
        private static void Awake(this UGFUIForm self, int uiFormId, ETMonoUIForm ugfETUIForm)
        {
            self.OnAwake(uiFormId, ugfETUIForm);
        }

        [EntitySystem]
        private static void Destroy(this UGFUIForm self)
        {
            self.OnDestroy();
        }

        public static void AddUIWidget<T>(this UGFUIForm self, Transform transform) where T : IUGFUIWidgetEvent
        {
            self.AddChild<UGFUIWidget, Transform>(transform, true);
        }
    }
}