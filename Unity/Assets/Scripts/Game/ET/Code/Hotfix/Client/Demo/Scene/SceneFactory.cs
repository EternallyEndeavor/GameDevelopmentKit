using System.Net.Sockets;
using Cysharp.Threading.Tasks;

namespace ET.Client
{
    public static partial class SceneFactory
    {
        public static async UniTask<Scene> CreateClientScene(int zone, SceneType sceneType, string name)
        {
            await UniTask.CompletedTask;
            
            Scene clientScene = EntitySceneFactory.CreateScene(zone, sceneType, name, ClientSceneManagerComponent.Instance);
            clientScene.AddComponent<ObjectWait>();
            clientScene.AddComponent<PlayerComponent>();
            clientScene.AddComponent<CurrentScenesComponent>();
            
            EventSystem.Instance.Publish(clientScene, new EventType.AfterCreateClientScene());
            return clientScene;
        }
        
        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;
            
            EventSystem.Instance.Publish(currentScene, new EventType.AfterCreateCurrentScene());
            return currentScene;
        }
    }
}