using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PostProcessor.Core.GCodes;

namespace PostProcessor.Tests;

[TestFixture]
public class GCodeParameterParser_Tests
{
    public class TestGCodeCommand : BaseGCodeCommand
    {
        public TestGCodeCommand(string originalStatement) : base(originalStatement)
        {
        }

        public IReadOnlyList<string> GetParameters() => GetParameters(OriginalStatement);
    }

    protected string Input = string.Empty;
    protected IReadOnlyList<string> Output = Array.Empty<string>();
    protected TestGCodeCommand Subject;

    public GCodeParameterParser_Tests() 
        => Subject = new TestGCodeCommand(Input);

    [SetUp]
    public void Because() => Output = Subject.GetParameters();

    public class When_parsing_a_command_with_no_parameters : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_no_parameters() => Input = "G29";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().BeEmpty();
    }

    public class When_parsing_a_command_with_one_parameter_no_spaces : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_one_parameter_no_spaces() => Input = "G1X10";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().HaveCount(1);

        [Test]
        public void It_should_capture_the_correct_parameter() => Output[0].Should().Be("X10");
    }

    public class When_parsing_a_command_with_two_parameters_no_spaces : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_two_parameters_no_spaces() => Input = "G1X10Y10";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().HaveCount(2);

        [Test]
        public void It_should_capture_the_correct_parameter_first() => Output[0].Should().Be("X10");

        [Test]
        public void It_should_capture_the_correct_parameter_second() => Output[1].Should().Be("Y10");
    }

    public class When_parsing_a_command_with_one_parameter_with_spaces : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_one_parameter_with_spaces() => Input = "G1 X10 ";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().HaveCount(1);

        [Test]
        public void It_should_capture_the_correct_parameter_first() => Output[0].Should().Be("X10");
    }

    public class When_parsing_a_command_with_two_parameters_with_spaces : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_two_parameters_with_spaces() => Input = "G1 X10.30 Y10";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().HaveCount(2);

        [Test]
        public void It_should_capture_the_correct_parameter_first() => Output[0].Should().Be("X10.30");

        [Test]
        public void It_should_capture_the_correct_parameter_second() => Output[1].Should().Be("Y10");
    }

    public class When_parsing_a_command_with_many_parameters_and_comment : GCodeParameterParser_Tests
    {
        public When_parsing_a_command_with_many_parameters_and_comment() => Input = "G239 X99.5 Y15.839238 ; This is an otherwise complex X99.5 thing";

        [Test]
        public void It_should_have_correct_parameter_count() => Output.Should().HaveCount(3);

        [Test]
        public void It_should_capture_the_correct_parameter_first() => Output[0].Should().Be("X99.5");

        [Test]
        public void It_should_capture_the_correct_parameter_second() => Output[1].Should().Be("Y15.839238");

        [Test]
        public void It_should_capture_the_comment() => Output[2].Should().Be("; This is an otherwise complex X99.5 thing");
    }
}