namespace ModelTest;

public class TestSuitesTest : ModelTestsSetup
{
    [TestCase("FoodOrderingSite-efsm_trans-R-transition-75-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-R-transition-50-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-R-state-75-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-R-state-50-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-TT-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-AS-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-ATS0-test_suite.json")]
    [TestCase("FoodOrderingSite-efsm_trans-NS-1-test_suite.json")]
    public async Task Run_Efsm_TestSuite(string fileName)
    {
        var suite = TestSuiteLoader.Load(fileName);

        Assert.That(suite.input_list, Has.Count.EqualTo(suite.output_list.Count),
            "input_list and output_list must have the same length.");

        await Page.GotoAsync(AppBaseUrl);

        for (int i = 0; i < suite.input_list.Count; i++)
        {
            var input = suite.input_list[i];
            var expected = suite.output_list[i];

            if (!inputs.TryGetValue(input, out var action))
                Assert.Fail($"No action defined for EFSM input '{input}' at step {i}.");

            if (!outputs.TryGetValue(expected, out var assertion))
                Assert.Fail($"No assertion defined for EFSM output '{expected}' at step {i}.");

            await TestContext.Progress.WriteLineAsync($"Step {i}: {input} → {expected}");

            await action();
            await assertion();
        }
    }
}