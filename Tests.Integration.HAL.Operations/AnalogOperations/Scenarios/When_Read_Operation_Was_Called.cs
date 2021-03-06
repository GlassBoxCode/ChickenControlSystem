﻿using Bootstrapping.Services.Contract.HAL.Enum;
using HAL.Models.Device;
using NUnit.Framework;

namespace Tests.Integration.HAL.Operations.AnalogOperations.Scenarios
{
    public class When_Read_Operation_Was_Called : Given_Operation_Was_Called
    {
        [Test]
        public void Then_200_Is_Read_Successfully()
        {
            var result = SUT.Read(new LightAnolougeSensor());
            Assert.AreEqual(OperationResultEnum.Succeess, result.ResultStatus);
            Assert.AreEqual(200, result.Return);
        }
    }
}