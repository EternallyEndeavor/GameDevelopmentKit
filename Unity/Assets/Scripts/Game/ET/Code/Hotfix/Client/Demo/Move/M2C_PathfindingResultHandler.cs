﻿using Cysharp.Threading.Tasks;

namespace ET.Client
{
	[MessageHandler(SceneType.Demo)]
	public class M2C_PathfindingResultHandler : MessageHandler<M2C_PathfindingResult>
	{
		protected override async UniTask Run(Session session, M2C_PathfindingResult message)
		{
			Unit unit = session.DomainScene().CurrentScene().GetComponent<UnitComponent>().Get(message.Id);

			float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);

			await unit.GetComponent<MoveComponent>().MoveToAsync(message.Points, speed);
		}
	}
}
