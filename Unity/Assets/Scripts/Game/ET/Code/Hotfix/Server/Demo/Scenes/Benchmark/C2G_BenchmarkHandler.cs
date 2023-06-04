﻿using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{
    [MessageHandler(SceneType.BenchmarkServer)]
    public class C2G_BenchmarkHandler: MessageHandler<C2G_Benchmark, G2C_Benchmark>
    {
        protected override async UniTask Run(Session session, C2G_Benchmark request, G2C_Benchmark response)
        {
            using C2G_Benchmark _ = request;
            BenchmarkServerComponent benchmarkServerComponent = session.DomainScene().GetComponent<BenchmarkServerComponent>();
            if (benchmarkServerComponent.Count++ % 1000000 == 0)
            {
                Log.Debug($"benchmark count: {benchmarkServerComponent.Count} {TimeHelper.ClientNow()}");
            }
            await UniTask.CompletedTask;
        }
    }
}