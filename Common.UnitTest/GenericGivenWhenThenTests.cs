﻿namespace UnitTest
{
    public abstract class GenericGivenWhenThenTests<T> : GivenWhenThenTests
    {
        protected T SUT;
    }
}