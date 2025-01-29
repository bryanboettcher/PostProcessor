using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PostProcessor.Core.GCodes;
using PostProcessor.Core.GCodes.Core;
using PostProcessor.Core.Streaming;

namespace PostProcessor.Tests;

[TestFixture]
public class MotionAwareGCodeStreamer_Tests
{
    protected GenericGCodeStatement Input;
    protected GenericGCodeStatement? Output;

    protected MotionAwareGCodeStreamer Subject;

    public MotionAwareGCodeStreamer_Tests()
    {
        Subject = new MotionAwareGCodeStreamer();
    }

    [SetUp]
    public void Because() => Output = Subject.Process([ Input ]).FirstOrDefault();

    public class When_processing_G0 : MotionAwareGCodeStreamer_Tests
    {
        public When_processing_G0()
        {
            Input = new GenericGCodeStatement("G0 X1 Y2 Z3");
        }

        [Test] 
        public void It_should_return_a_statement() => Assert.That(Output, Is.Not.Null);

        [Test]
        public void It_should_produce_a_rapid_move() => Assert.That(Output, Is.TypeOf<RapidMoveCommand>());

        [Test]
        public void It_should_keep_X_value() => Assert.Fail("change this to an assertion that ((RapidMoveCommand)Output).X is the right value");

        [Test]
        public void It_should_keep_Y_value() => Assert.Fail("change this to an assertion that ((RapidMoveCommand)Output).Y is the right value");

        [Test]
        public void It_should_keep_Z_value() => Assert.Fail("change this to an assertion that ((RapidMoveCommand)Output).Z is the right value");
    }

    public class When_processing_G1 : MotionAwareGCodeStreamer_Tests { }
}
