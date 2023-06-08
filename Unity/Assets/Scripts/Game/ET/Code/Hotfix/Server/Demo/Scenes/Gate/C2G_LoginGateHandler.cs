﻿using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_LoginGateHandler : MessageHandler<C2G_LoginGate, G2C_LoginGate>
    {
        protected override async UniTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response)
        {
            Scene scene = session.DomainScene();
            string account = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (account == null)
            {
                response.Error = ErrorCore.ERR_ConnectGateKeyError;
                response.Message = "Gate key验证失败!";
                return;
            }
            
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
            Player player = playerComponent.GetByAccount(account);
            if (player == null)
            {
                player = playerComponent.AddChild<Player, string>(account);
                playerComponent.Add(player);
                PlayerSessionComponent playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
                playerSessionComponent.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                await playerSessionComponent.AddLocation(LocationType.GateSession);
			
                player.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);
                await player.AddLocation(LocationType.Player);
			
                session.AddComponent<SessionPlayerComponent>().Player = player;
                playerSessionComponent.Session = session;
            }
            else
            {
                // 判断是否在战斗
                PlayerRoomComponent playerRoomComponent = player.GetComponent<PlayerRoomComponent>();
                if (playerRoomComponent.RoomInstanceId != 0)
                {
                    CheckRoom(player, session).Forget();
                }
                else
                {
                    PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
                    playerSessionComponent.Session = session;
                }
            }

            response.PlayerId = player.Id;
            await UniTask.CompletedTask;
        }

        private static async UniTask CheckRoom(Player player, Session session)
        {
            await Game.WaitFrameFinish();
            
            Room2G_Reconnect room2GateReconnect = await ActorMessageSenderComponent.Instance.Call(
                player.GetComponent<PlayerRoomComponent>().RoomInstanceId,
                new G2Room_Reconnect() { PlayerId = player.Id }) as Room2G_Reconnect;
            session.Send(new G2C_Reconnect() { StartTime = room2GateReconnect.StartTime, UnitInfos = room2GateReconnect.UnitInfos, Frame = room2GateReconnect.Frame});
            session.AddComponent<SessionPlayerComponent>().Player = player;
            player.GetComponent<PlayerSessionComponent>().Session = session;
        }
    }
}