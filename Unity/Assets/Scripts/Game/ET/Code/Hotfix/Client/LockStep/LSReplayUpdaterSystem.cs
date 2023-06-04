using System;

namespace ET.Client
{
    [FriendOf(typeof(LSReplayUpdater))]
    public static partial class LSReplayUpdaterSystem
    {
        [EntitySystem]
        private class LSReplayUpdaterUpdateSystem : UpdateSystem<LSReplayUpdater>
        {
            protected override void Update(LSReplayUpdater self)
            {
                Room room = self.GetParent<Room>();
                long timeNow = TimeHelper.ServerNow();

                int i = 0;
                while (true)
                {
                    if (room.AuthorityFrame + 1 >= room.Replay.FrameInputs.Count)
                    {
                        break;
                    }
                
                    if (timeNow < room.FixedTimeCounter.FrameTime(room.AuthorityFrame + 1))
                    {
                        break;
                    }

                    ++room.AuthorityFrame;

                    OneFrameInputs oneFrameInputs = room.Replay.FrameInputs[room.AuthorityFrame];
            
                    room.Update(oneFrameInputs);
                    room.SpeedMultiply = ++i;
                
                    long timeNow2 = TimeHelper.ServerNow();
                    if (timeNow2 - timeNow > 5)
                    {
                        break;
                    }
                }
            }
        }

        public static void ChangeReplaySpeed(this LSReplayUpdater self)
        {
            Room room = self.Room();
            LSReplayUpdater lsReplayUpdater = room.GetComponent<LSReplayUpdater>();
            if (lsReplayUpdater.ReplaySpeed == 8)
            {
                lsReplayUpdater.ReplaySpeed = 1;
            }
            else
            {
                lsReplayUpdater.ReplaySpeed *= 2;
            }
            
            int updateInterval = LSConstValue.UpdateInterval / lsReplayUpdater.ReplaySpeed;
            room.FixedTimeCounter.ChangeInterval(updateInterval, room.AuthorityFrame);
        }
    }
}