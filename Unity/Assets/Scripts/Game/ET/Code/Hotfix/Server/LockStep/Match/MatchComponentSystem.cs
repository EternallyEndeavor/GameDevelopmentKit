using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ET.Server
{

    [FriendOf(typeof(MatchComponent))]
    public static partial class MatchComponentSystem
    {
        public static async UniTask Match(this MatchComponent self, long playerId)
        {
            if (self.waitMatchPlayers.Contains(playerId))
            {
                return;
            }
            
            self.waitMatchPlayers.Add(playerId);

            if (self.waitMatchPlayers.Count < LSConstValue.MatchCount)
            {
                return;
            }
            
            // 申请一个房间
            DRStartSceneConfig startSceneConfig = RandomGenerator.RandomArray(Tables.Instance.DTStartSceneConfig.Maps);
            Match2Map_GetRoom match2MapGetRoom = new() {PlayerIds = new List<long>()};
            foreach (long id in self.waitMatchPlayers)
            {
                match2MapGetRoom.PlayerIds.Add(id);
            }
            self.waitMatchPlayers.Clear();
            
            Map2Match_GetRoom map2MatchGetRoom = await ActorMessageSenderComponent.Instance.Call(
                startSceneConfig.InstanceId, match2MapGetRoom) as Map2Match_GetRoom;

            Match2G_NotifyMatchSuccess match2GNotifyMatchSuccess = new() { InstanceId = map2MatchGetRoom.InstanceId };
            foreach (long id in match2MapGetRoom.PlayerIds) // 这里发送消息线程不会修改PlayerInfo，所以可以直接使用
            {
                ActorLocationSenderComponent.Instance.Get(LocationType.Player).Send(id, match2GNotifyMatchSuccess);
                // 等待进入房间的确认消息，如果超时要通知所有玩家退出房间，重新匹配
            }
        }
    }
}