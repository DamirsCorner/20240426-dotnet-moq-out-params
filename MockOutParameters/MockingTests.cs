using FluentAssertions;
using Moq;

namespace MockOutParameters;

public class MockingTests
{
    [Test]
    public void CanStaticallyMockMethodWithRegularParameter()
    {
        var dependencyMock = new Mock<IDependency>();
        dependencyMock.Setup(x => x.MethodWithNoOutParameter(It.IsAny<string>())).Returns("output");

        var result = dependencyMock.Object.MethodWithNoOutParameter("input");

        result.Should().Be("output");
    }

    [Test]
    public void CanDynamicallyMockMethodWithRegularParameter()
    {
        var dependencyMock = new Mock<IDependency>();
        dependencyMock
            .Setup(x => x.MethodWithNoOutParameter(It.IsAny<string>()))
            .Returns<string>(p => p);

        var result = dependencyMock.Object.MethodWithNoOutParameter("input");

        result.Should().Be("input");
    }

    [Test]
    public void CanStaticallyMockMethodWithOutParameter()
    {
        var dependencyMock = new Mock<IDependency>();
        var outValue = "output";
        dependencyMock
            .Setup(x => x.MethodWithOutParameter(It.IsAny<string>(), out outValue))
            .Returns(true);

        var result = dependencyMock.Object.MethodWithOutParameter("input", out var outResult);

        result.Should().BeTrue();
        outResult.Should().Be("output");
    }

    [Test]
    public void CanDynamicallyMockMethodWithOutParameter()
    {
        var dependencyMock = new Mock<IDependency>();
        dependencyMock
            .Setup(x => x.MethodWithOutParameter(It.IsAny<string>(), out It.Ref<string>.IsAny))
            .Returns(
                (string input, out string outParam) =>
                {
                    outParam = input;
                    return input.Length > 5;
                }
            );

        var result = dependencyMock.Object.MethodWithOutParameter("input", out var outResult);

        result.Should().BeFalse();
        outResult.Should().Be("input");
    }
}
