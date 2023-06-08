﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MongoDB.Bson;

namespace ET.Server
{
    public static partial class TransferHelper
    {
        public static async UniTask TransferAtFrameFinish(Unit unit, long sceneInstanceId, string sceneName)
        {
            await Game.WaitFrameFinish();

            await TransferHelper.Transfer(unit, sceneInstanceId, sceneName);
        }
        

        public static async UniTask Transfer(Unit unit, long sceneInstanceId, string sceneName)
        {
            // location加锁
            long unitId = unit.Id;
            long unitInstanceId = unit.InstanceId;
            
            M2M_UnitTransferRequest request = new M2M_UnitTransferRequest() {Entitys = new List<byte[]>()};
            request.OldInstanceId = unitInstanceId;
            request.Unit = unit.ToBson();
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransfer)
                {
                    request.Entitys.Add(entity.ToBson());
                }
            }
            unit.Dispose();
            
            await LocationProxyComponent.Instance.Lock(LocationType.Unit, unitId, unitInstanceId);
            await ActorMessageSenderComponent.Instance.Call(sceneInstanceId, request);
        }
    }
}