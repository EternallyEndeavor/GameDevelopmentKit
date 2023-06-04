using System;
using System.IO;

namespace ET.Client
{
    [FriendOf(typeof (LSClientUpdater))]
    public static partial class LSClientUpdaterSystem
    {
        [EntitySystem]
        private class LSClientUpdaterAwakeSystem : AwakeSystem<LSClientUpdater>
        {
            protected override void Awake(LSClientUpdater self)
            {
                Room room = self.GetParent<Room>();
                self.MyId = room.GetParent<Scene>().GetComponent<PlayerComponent>().MyId;
            }
        }

        [EntitySystem]
        private class LSClientUpdaterUpdateSystem : UpdateSystem<LSClientUpdater>
        {
            protected override void Update(LSClientUpdater self)
            {
                Room room = self.GetParent<Room>();
                long timeNow = TimeHelper.ServerNow();
                Scene clientScene = room.GetParent<Scene>();

                int i = 0;
                while (true)
                {
                    if (timeNow < room.FixedTimeCounter.FrameTime(room.PredictionFrame + 1))
                    {
                        return;
                    }

                    // 最多只预测5帧
                    if (room.PredictionFrame - room.AuthorityFrame > 5)
                    {
                        return;
                    }

                    ++room.PredictionFrame;
                    OneFrameInputs oneFrameInputs = self.GetOneFrameMessages(room.PredictionFrame);
                
                    room.Update(oneFrameInputs);
                    room.SendHash(room.PredictionFrame);
                
                    room.SpeedMultiply = ++i;

                    FrameMessage frameMessage = FrameMessage.Create(true);
                    frameMessage.Frame = room.PredictionFrame;
                    frameMessage.Input = self.Input;
                    clientScene.GetComponent<SessionComponent>().Session.Send(frameMessage);
                
                    long timeNow2 = TimeHelper.ServerNow();
                    if (timeNow2 - timeNow > 5)
                    {
                        break;
                    }
                }
            }
        }

        private static OneFrameInputs GetOneFrameMessages(this LSClientUpdater self, int frame)
        {
            Room room = self.GetParent<Room>();
            FrameBuffer frameBuffer = room.FrameBuffer;
            
            if (frame <= room.AuthorityFrame)
            {
                return frameBuffer.FrameInputs(frame);
            }
            
            // predict
            OneFrameInputs predictionFrame = frameBuffer.FrameInputs(frame);
            
            frameBuffer.MoveForward(frame);
            if (frameBuffer.CheckFrame(room.AuthorityFrame))
            {
                OneFrameInputs authorityFrame = frameBuffer.FrameInputs(room.AuthorityFrame);
                authorityFrame.CopyTo(predictionFrame);
            }
            predictionFrame.Inputs[self.MyId] = self.Input;
            
            return predictionFrame;
        }
    }
}