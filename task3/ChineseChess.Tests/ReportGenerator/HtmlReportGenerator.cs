using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace ChineseChess.Tests.ReportGenerator
{
    public static class HtmlReportGenerator
    {
        public static void GenerateReport(string trxFilePath, string outputHtmlPath)
        {
            var trx = XDocument.Load(trxFilePath);
            XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

            var results = trx.Descendants(ns + "UnitTestResult")
                .Select(r => new
                {
                    TestName = r.Attribute("testName")?.Value,
                    Outcome = r.Attribute("outcome")?.Value,
                    Duration = r.Attribute("duration")?.Value,
                    ErrorMessage = r.Descendants(ns + "Message").FirstOrDefault()?.Value,
                    StackTrace = r.Descendants(ns + "StackTrace").FirstOrDefault()?.Value
                })
                .ToList();

            var summary = trx.Descendants(ns + "ResultSummary").First();
            var total = summary.Descendants(ns + "Counters").First().Attribute("total")?.Value;
            var passed = summary.Descendants(ns + "Counters").First().Attribute("passed")?.Value;
            var failed = summary.Descendants(ns + "Counters").First().Attribute("failed")?.Value;

            var html = new XDocument(
                new XElement("html",
                    new XElement("head",
                        new XElement("title", "Test Report"),
                        new XElement("style", GetStyles())
                    ),
                    new XElement("body",
                        new XElement("h1", "Test Execution Report"),
                        new XElement("h2", "Summary"),
                        new XElement("p", $"Total Tests: {total}"),
                        new XElement("p", $"Passed: {passed}"),
                        new XElement("p", $"Failed: {failed}"),
                        new XElement("h2", "Results"),
                        new XElement("table",
                            new XElement("thead",
                                new XElement("tr",
                                    new XElement("th", "Test Name"),
                                    new XElement("th", "Outcome"),
                                    new XElement("th", "Duration")
                                )
                            ),
                            new XElement("tbody",
                                from res in results
                                select new XElement("tr",
                                    new XAttribute("class", res.Outcome?.ToLower()),
                                    new XElement("td", res.TestName),
                                    new XElement("td", res.Outcome),
                                    new XElement("td", res.Duration)
                                )
                            )
                        )
                    )
                )
            );

            File.WriteAllText(outputHtmlPath, html.ToString());
        }

        private static string GetStyles()
        {
            return @"
                body { font-family: sans-serif; }
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid #dddddd; text-align: left; padding: 8px; }
                th { background-color: #f2f2f2; }
                tr.passed { background-color: #90ee90; }
                tr.failed { background-color: #f08080; }
            ";
        }
    }
} 