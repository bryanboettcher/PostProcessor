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
            Input = new StandardCommandGCode("G0 X1 Y2 Z3 F1200");
        }

        [Test] 
        public void It_should_return_a_statement() => Assert.That(Output, Is.Not.Null);

        [Test]
        public void It_should_produce_a_rapid_move() => Assert.That(Output, Is.TypeOf<RapidMoveCommand>());

        [Test]
        public void It_should_keep_X_value() => Assert.That(((RapidMoveCommand)Output!).X, Is.EqualTo(1));

        [Test]
        public void It_should_keep_Y_value() => Assert.That(((RapidMoveCommand)Output!).Y, Is.EqualTo(2));

        [Test]
        public void It_should_keep_Z_value() => Assert.That(((RapidMoveCommand)Output!).Z, Is.EqualTo(3));

        [Test]
        public void It_should_keep_F_value() => Assert.That(((RapidMoveCommand)Output!).Velocity, Is.EqualTo(1200));
    }

    public class When_processing_G1 : MotionAwareGCodeStreamer_Tests
    {
        public When_processing_G1()
        {
            Input = new StandardCommandGCode("G1 X1 Y2 Z3 E10 F1200");
        }

        [Test]
        public void It_should_return_a_statement() => Assert.That(Output, Is.Not.Null);

        [Test]
        public void It_should_produce_an_interpolated_move() => Assert.That(Output, Is.TypeOf<InterpolatedMoveCommand>());

        [Test]
        public void It_should_keep_X_value() => Assert.That(((InterpolatedMoveCommand)Output!).X, Is.EqualTo(1));

        [Test]
        public void It_should_keep_Y_value() => Assert.That(((InterpolatedMoveCommand)Output!).Y, Is.EqualTo(2));

        [Test]
        public void It_should_keep_Z_value() => Assert.That(((InterpolatedMoveCommand)Output!).Z, Is.EqualTo(3));

        [Test]
        public void It_should_keep_F_value() => Assert.That(((InterpolatedMoveCommand)Output!).Velocity, Is.EqualTo(1200));

        [Test]
        public void It_should_keep_E_value() => Assert.That(((InterpolatedMoveCommand)Output!).E, Is.EqualTo(10));
    }
}
