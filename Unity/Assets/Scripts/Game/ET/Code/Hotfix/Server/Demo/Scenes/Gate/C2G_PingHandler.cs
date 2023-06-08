﻿using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_PingHandler : MessageHandler<C2G_Ping, G2C_Ping>
	{
		protected override async UniTask Run(Session session, C2G_Ping request, G2C_Ping response)
		{
			response.Time = TimeHelper.ServerNow();
			await UniTask.CompletedTask;
		}
	}
}