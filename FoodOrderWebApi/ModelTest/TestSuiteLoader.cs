using System.Text.Json;
using ModelTest.Models;

namespace ModelTest;

public class TestSuiteLoader
{
    public static TestSuite Load(string fileName)
    {
        var baseDir = TestContext.CurrentContext.TestDirectory;

        var fullPath = Path.Combine(baseDir, "TestSuites", fileName);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Cannot find test suite JSON at {fullPath}");

        var json = File.ReadAllText(fullPath);

        using var doc = JsonDocument.Parse(json);

        var suiteElement = doc.RootElement.GetProperty("test_suite");

        var testSuite = JsonSerializer.Deserialize<TestSuite>(
            suiteElement.GetRawText(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (testSuite == null)
            throw new InvalidOperationException("Invalid test suite JSON: missing 'test_suite' root.");

        return testSuite;
    }
}