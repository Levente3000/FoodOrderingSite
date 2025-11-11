namespace ModelTest.Models;

public class TestSuite
{
    public string name { get; set; }
    public string id { get; set; }
    public string method { get; set; }
    public SuiteParameters parameters { get; set; }
    public List<string> input_list { get; set; }
    public List<string> output_list { get; set; }
}