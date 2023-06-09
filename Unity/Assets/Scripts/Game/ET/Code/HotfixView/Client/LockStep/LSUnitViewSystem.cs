using System;
using TrueSync;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(LSUnitView))]
    public static partial class LSUnitViewSystem
    {
        [EntitySystem]
        private class LSUnitViewAwakeSystem : AwakeSystem<LSUnitView, GameObject>
        {
            protected override void Awake(LSUnitView self, GameObject go)
            {
                self.GameObject = go;
                self.Transform = go.transform;
            }
        }

        [LSEntitySystem]
        private class LSUnitViewLSRollbackSystem : LSRollbackSystem<LSUnitView>
        {
            protected override void LSRollback(LSUnitView self)
            {
                //LSUnit unit = self.GetUnit();
                //self.Transform.position = unit.Position.ToVector();
                //self.Transform.rotation = unit.Rotation.ToQuaternion();
                //self.t = 0;
                //self.totalTime = 0;
            }
        }

        [EntitySystem]
        private class LSUnitViewUpdateSystem : UpdateSystem<LSUnitView>
        {
            protected override void Update(LSUnitView self)
            {
                LSUnit unit = self.GetUnit();
            
                Vector3 unitPos = unit.Position.ToVector();
                const float speed = 6f;
                float speed2 = speed;// * self.Room().SpeedMultiply;
            
                if (unitPos != self.Position)
                {
                    float distance = (unitPos - self.Position).magnitude;
                    self.totalTime = distance / speed2;
                    self.t = 0;
                    self.Position = unit.Position.ToVector();
                    self.Rotation = unit.Rotation.ToQuaternion();
                }
            
            
                LSInput input = unit.GetComponent<LSInputComponent>().LSInput;
                if (input.V != TSVector2.zero)
                {
                    self.GetComponent<LSAnimatorComponent>().SetFloatValue("Speed", speed2);
                }
                else
                {
                    self.GetComponent<LSAnimatorComponent>().SetFloatValue("Speed", 0);
                }
                self.t += Time.deltaTime;
                self.Transform.rotation = Quaternion.Lerp(self.Transform.rotation, self.Rotation, self.t / 1f);
                self.Transform.position = Vector3.Lerp(self.Transform.position, self.Position, self.t / self.totalTime);
            }
        }

        private static LSUnit GetUnit(this LSUnitView self)
        {
            LSUnit unit = self.Unit;
            if (unit != null)
            {
                return unit;
            }

            self.Unit = (self.Domain as Room).LSWorld.GetComponent<LSUnitComponent>().GetChild<LSUnit>(self.Id);
            return self.Unit;
        }
    }
}