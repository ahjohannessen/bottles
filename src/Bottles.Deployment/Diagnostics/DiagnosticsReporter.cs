using System;
using Bottles.Deployment.Parsing;
using Bottles.Deployment.Runtime;
using FubuCore.CommandLine;
using FubuCore;

namespace Bottles.Deployment.Diagnostics
{
    public class DiagnosticsReporter : IDiagnosticsReporter
    {
        private readonly IDeploymentDiagnostics _diagnostics;

        public DiagnosticsReporter(IDeploymentDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void WriteReport(DeploymentOptions options, DeploymentPlan plan)
        {
            var report = new DeploymentReport("Deployment Report");
            report.WriteDeploymentPlan(plan);
            report.WriteLoggingSession(_diagnostics.Session);
        
            ConsoleWriter.Line();
            ConsoleWriter.PrintHorizontalLine();
            ConsoleWriter.Write("Writing deployment report to " + options.ReportName.ToFullPath());
            report.Document.WriteToFile(options.ReportName);
        }
    }
}