﻿using Cysharp.Threading.Tasks;
using Unity.Mathematics;

namespace ET.Client
{
	[MessageHandler(SceneType.Demo)]
	public class M2C_StopHandler : MessageHandler<M2C_Stop>
	{
		protected override async UniTask Run(Session session, M2C_Stop message)
		{
			Unit unit = session.DomainScene().CurrentScene().GetComponent<UnitComponent>().Get(message.Id);
			if (unit == null)
			{
				return;
			}

			MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
			moveComponent.Stop(message.Error == 0);
			unit.Position = message.Position;
			unit.Rotation = message.Rotation;
			unit.GetComponent<ObjectWait>()?.Notify(new Wait_UnitStop() {Error = message.Error});
			await UniTask.CompletedTask;
		}
	}
}
