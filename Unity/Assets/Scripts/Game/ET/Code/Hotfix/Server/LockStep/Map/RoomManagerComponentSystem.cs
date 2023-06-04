using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{

    public static partial class RoomManagerComponentSystem
    {
        public static async UniTask<Room> CreateServerRoom(this RoomManagerComponent self, Match2Map_GetRoom request)
        {
            await UniTask.CompletedTask;
            
            Room room = self.AddChild<Room>();
            room.Name = "Server";
            
            room.AddComponent<RoomServerComponent, Match2Map_GetRoom>(request);

            room.LSWorld = new LSWorld(SceneType.LockStepServer);

            room.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);
            
            return room;
        }
    }
}