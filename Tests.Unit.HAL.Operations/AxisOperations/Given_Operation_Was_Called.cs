﻿using Bootstrapping.Services.Contract.HAL.Interface;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Unit.HAL.Operations.AxisOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<global::HAL.Operations.AxisOperations>
    {
        protected IControlLine MockControlLine;
        protected IErrorService MockErrorService;

        protected override void Given()
        {
            MockControlLine = Substitute.For<IControlLine>();
            MockErrorService = Substitute.For<IErrorService>();

            SUT = new global::HAL.Operations.AxisOperations(
                MockErrorService,
                MockControlLine
            );
        }
    }
}