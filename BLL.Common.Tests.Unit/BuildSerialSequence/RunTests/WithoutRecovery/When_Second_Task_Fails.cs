﻿using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Common.Tests.Unit.BuildSerialSequence.RunTests.WithoutRecovery
{
    [TestFixture(1, 1)]
    [TestFixture(1, 3)]
    [TestFixture(3, 1)]
    [TestFixture(3, 3)]
    public class When_Second_Task_Fails : Given_A_SerialSequenceIsBuilt
    {
        private readonly int _runCountSecond;
        private readonly int _runCountFirst;
        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;

        public When_Second_Task_Fails(int runCountSecond, int runCountFirst)
        {
            _runCountSecond = runCountSecond;
            _runCountFirst = runCountFirst;
        }

        protected override void When()
        {
            _recoveryOptions = new RecoveryOptionsDto();

            MockFirstTask
                .RunCount
                .Returns(_runCountFirst);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockSecondTask
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockSecondTask
                .RunCount
                .Returns(_runCountSecond);
            MockSecondTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Fisrt_Task_Is_Run_Once()
        {
            MockFirstTask
                .Received(1)
                .Run();
        }

        [Test]
        public void Then_Second_Task_Is_Run_For_The_Ammount_Of_Times_Determined_In_The_Run_Count()
        {
            MockSecondTask
                .Received(_runCountSecond)
                .Run();
        }

        [Test]
        public void Then_Fail_Action_Is_Run_Once()
        {
            MockSecondTask
                .Received(1)
                .HandleFail();
        }

        [Test]
        public void Then_Seqeuence_Fails()
        {
            Assert.AreEqual(SequenceResultEnum.Fail, _result);
        }
    }
}