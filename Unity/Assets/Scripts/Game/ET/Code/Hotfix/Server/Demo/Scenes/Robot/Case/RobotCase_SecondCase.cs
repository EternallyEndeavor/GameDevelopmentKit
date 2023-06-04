using System;
using Cysharp.Threading.Tasks;

namespace ET.Server
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_TestRobotCase2Handler: MessageHandler<M2C_TestRobotCase2>
    {
        protected override async UniTask Run(Session session, M2C_TestRobotCase2 message)
        {
            ObjectWait objectWait = session.ClientScene().GetComponent<ObjectWait>();
            if (objectWait == null)
            {
                return;
            }
            objectWait.Notify(new RobotCase_SecondCaseWait {Error = WaitTypeError.Success, M2CTestRobotCase2 = message});
            await UniTask.CompletedTask;
        }
    }

    [Invoke(RobotCaseType.SecondCase)]
    public class RobotCase_SecondCase: ARobotCase
    {
        protected override async UniTask Run(RobotCase robotCase)
        {
            // 创建了1个机器人，生命周期是RobotCase
            Scene robotScene = await robotCase.NewRobot(1);

            ObjectWait objectWait = robotScene.GetComponent<ObjectWait>();
            robotScene.GetComponent<Client.SessionComponent>().Session.Send(new C2M_TestRobotCase2() {N = robotScene.Zone});
            RobotCase_SecondCaseWait robotCaseSecondCaseWait = await objectWait.Wait<RobotCase_SecondCaseWait>();
            if (robotCaseSecondCaseWait.M2CTestRobotCase2.N != robotScene.Zone)
            {
                throw new Exception($"robot case: {RobotCaseType.SecondCase} run fail!");
            }
        }
    }
}