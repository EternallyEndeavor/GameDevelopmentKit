using Cysharp.Threading.Tasks;

namespace ET.Server
{
    public static partial class RobotSceneFactory
    {
        public static async UniTask<Scene> Create(
            Entity parent,
            long id,
            long instanceId,
            int zone,
            string name,
            SceneType sceneType,
            DRStartSceneConfig startSceneConfig = null
        )
        {
            await UniTask.CompletedTask;
            Log.Info($"create scene: {sceneType} {name} {zone}");
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case SceneType.Robot:
                    scene.AddComponent<RobotManagerComponent>();
                    break;
            }

            return scene;
        }
    }
}