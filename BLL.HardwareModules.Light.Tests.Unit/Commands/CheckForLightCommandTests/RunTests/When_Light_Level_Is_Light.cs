﻿using Crosscutting.Contract.HAL.Dto;
using Crosscutting.Contract.HAL.Enum;
using Crosscutting.Contract.HAL.Interface;
using Crosscutting.Sequencing.Sequence;
using NSubstitute;
using NUnit.Framework;

namespace BLL.HardwareModules.Light.Tests.Unit.Commands.CheckForLightCommandTests.RunTests
{
    [TestFixture(100)]
    [TestFixture(200)]
    [TestFixture(400)]
    public class When_Light_Level_Is_Light : Given_CheckForLightCommand
    {
        private readonly int _lightLuxLevel;
        private SequenceResultEnum _result;

        public When_Light_Level_Is_Light(int lightLuxLevel)
        {
            _lightLuxLevel = lightLuxLevel;
        }

        public override void When()
        {
            MockAnalogOperations
                .Read(Arg.Any<IDevice>())
                .Returns(new OperationResult
                {
                    ResultStatus = OperationResultEnum.Succeess,
                    Return = _lightLuxLevel
                });
            MockValidateOperationService
                .GetSequenceResult(Arg.Any<OperationResultEnum>())
                .Returns(SequenceResultEnum.Success);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Command_Is_Successful()
        {
            Assert.AreEqual(SequenceResultEnum.Success, _result);
        }

        [Test]
        public void Then_No_Delay_Occurs()
        {
            MockThreadingOperations
                .DidNotReceive()
                .SyncronousDelay(Arg.Any<double>());
        }

        [Test]
        public void Then_Light_Value_Is_Read_Once()
        {
            MockAnalogOperations
                .Received()
                .Read(Arg.Is(MockLightSensor));
            MockAnalogOperations
                .Received(1)
                .Read(Arg.Any<IDevice>());
        }
    }
}