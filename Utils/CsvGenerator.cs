using System.Text;
using ToucanTesting.Models;

namespace ToucanTesting.Utils
{
    public class CsvGenerator
    {
        public string GenerateFromSuite(TestSuite suite)
        {
            var builder = new StringBuilder();
            builder.Append("Suite, Module, Last Tested, Automated, Automation ID, Criteria, Priority, Case, Expected Results, Conditions, Steps\r\n");

            foreach (TestModule tm in suite.TestModules)
            {
                foreach (TestCase tc in tm.TestCases)
                {
                    var suiteName = suite.Name.Replace("\"", string.Empty);
                    var moduleName = tm.Name.Replace("\"", string.Empty);
                    var lastTested = (tc.LastTested.HasValue) ? tc.LastTested.Value.ToString("mm/dd/yyyy") : "Never Tested";
                    var isAutomated = tc.IsAutomated ? "Yes" : "No";
                    var hasCriteria = tc.HasCriteria ? "Yes" : "No";
                    var caseDescription = tc.Description.Replace("\"", string.Empty);

                    builder.Append($"\"{suiteName}\",");
                    builder.Append($"\"{moduleName}\",");
                    builder.Append($"\"{lastTested}\",");
                    builder.Append($"\"{isAutomated}\",");
                    builder.Append($"\"{tc.AutomationId}\",");
                    builder.Append($"\"{hasCriteria}\",");
                    builder.Append($"\"{tc.Priority.ToString()}\",");
                    builder.Append($"\"{caseDescription}\",");

                    var erList = new StringBuilder();
                    erList.Append("\"");
                    foreach (ExpectedResult er in tc.ExpectedResults)
                    {
                        er.Description = er.Description.Replace("\"", string.Empty);
                        erList.Append($"* {er.Description}");
                    }
                    erList.Append("\"");
                    erList.ToString();
                    builder.Append($"{erList},");

                    var conditionsList = new StringBuilder();
                    conditionsList.Append("\"");
                    foreach (TestCondition c in tc.TestConditions)
                    {
                        c.Description = c.Description.Replace("\"", string.Empty);
                        conditionsList.Append($"* {c.Description}");
                    }
                    conditionsList.Append("\"");
                    conditionsList.ToString();
                    builder.Append($"{conditionsList},");

                    var actionList = new StringBuilder();
                    actionList.Append("\"");
                    foreach (TestAction a in tc.TestActions)
                    {
                        a.Description = a.Description.Replace("\"", string.Empty);
                        actionList.Append($"* {a.Description}");
                    }
                    actionList.Append("\"");
                    actionList.ToString();
                    builder.Append($"{actionList},\r\n");
                }
            }
            return builder.ToString();
        }
    }
}