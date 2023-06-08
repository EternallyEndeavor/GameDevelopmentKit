﻿using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class Match2G_NotifyMatchSuccessHandler : ActorMessageHandler<Player, Match2G_NotifyMatchSuccess>
	{
		protected override async UniTask Run(Player player, Match2G_NotifyMatchSuccess message)
		{
			player.AddComponent<PlayerRoomComponent>().RoomInstanceId = message.InstanceId;
			
			player.GetComponent<PlayerSessionComponent>().Session.Send(message);
			await UniTask.CompletedTask;
		}
	}
}