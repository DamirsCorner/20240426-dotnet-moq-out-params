namespace MockOutParameters;

public interface IDependency
{
    string MethodWithNoOutParameter(string input);
    bool MethodWithOutParameter(string input, out string outParameter);
}
