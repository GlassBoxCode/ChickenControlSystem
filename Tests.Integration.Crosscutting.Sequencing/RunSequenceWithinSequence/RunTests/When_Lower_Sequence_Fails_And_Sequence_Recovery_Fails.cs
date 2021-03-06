﻿using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Integration.Crosscutting.Sequencing.RunSequenceWithinSequence.RunTests
{
    public class When_Lower_Sequence_Fails_And_Sequence_Recovery_Fails : Given_Two_Sequences_Are_Built
    {
        private SequenceResultEnum _result;

        protected override void When()
        {
            MockLowerSequenceTask
                .Run()
                .Returns(SequenceResultEnum.Fail);
            MockLowerSequenceTask
                .RecoveryOptions
                .Returns(new RecoveryOptionsDto());
            MockLowerSequenceRecoveryTask
                .Run()
                .Returns(SequenceResultEnum.Fail);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Task_In_Lower_Sequence_Ran_5_Times()
        {
            MockLowerSequenceTask
                .Received(5)
                .Run();
        }

        [Test]
        public void Then_Second_Task_In_Upper_Sequence_Didnt_Run()
        {
            MockUpperSequenceSecondTask
                .DidNotReceive()
                .Run();
        }

        [Test]
        public void Then_Sequence_Fails()
        {
            Assert.AreEqual(_result, SequenceResultEnum.Fail);
        }
    }
}