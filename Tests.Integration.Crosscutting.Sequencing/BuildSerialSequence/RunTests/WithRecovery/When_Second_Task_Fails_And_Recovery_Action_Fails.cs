﻿using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.Crosscutting.Sequencing.BuildSerialSequence.RunTests.WithRecovery
{
    [TestFixture(1, 1)]
    [TestFixture(1, 3)]
    [TestFixture(3, 1)]
    [TestFixture(3, 3)]
    public class When_Second_Task_Fails_And_Recovery_Action_Fails : Given_A_Serial_Sequence_Is_Built
    {
        private readonly int _runCountSecond;
        private readonly int _runCountFirst;

        private SequenceResultEnum _result;
        private RecoveryOptionsDto _recoveryOptions;

        private IRunnable _mockRecoveryTask;

        public When_Second_Task_Fails_And_Recovery_Action_Fails(int runCountSecond, int runCountFirst)
        {
            _runCountSecond = runCountSecond;
            _runCountFirst = runCountFirst;
        }

        protected override void When()
        {
            _mockRecoveryTask = Substitute.For<IRunnable>();
            _mockRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Fail);
            _recoveryOptions = new RecoveryOptionsDto(true, _mockRecoveryTask.Run);

            MockFirstTask
                .GetRunCount()
                .Returns(_runCountFirst);
            MockFirstTask
                .Run()
                .Returns(SequenceResultEnum.Success);

            MockSecondTask
                .RecoveryOptions
                .Returns(_recoveryOptions);
            MockSecondTask
                .GetRunCount()
                .Returns(_runCountSecond);
            MockSecondTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Recovery_Action_Is_Run_Once()
        {
            _mockRecoveryTask
                .Received(1)
                .Run();
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