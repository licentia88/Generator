using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Generator.Components.Analyzers;


public static class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor BothOptionsSet = new DiagnosticDescriptor(
        "CC0001",
        "Both Datasource and Service parameters cannot be set simultaneously.",
        "Both Datasource and Service parameters cannot be set simultaneously.",
        "Design",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class CustomComponentAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(DiagnosticDescriptors.BothOptionsSet);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze);

        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ObjectCreationExpression);
    }

    private static void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var objectCreationExpression = (ObjectCreationExpressionSyntax)context.Node;

        var optionAArgument = objectCreationExpression.ArgumentList.Arguments.FirstOrDefault(arg =>
            arg.NameColon != null && arg.NameColon.Name.Identifier.Text == "DataSource");

        var optionBArgument = objectCreationExpression.ArgumentList.Arguments.FirstOrDefault(arg =>
            arg.NameColon != null && arg.NameColon.Name.Identifier.Text == "ServiceMethod");

        if (optionAArgument != null && optionBArgument != null)
        {
            var diagnostic = Diagnostic.Create(
                DiagnosticDescriptors.BothOptionsSet,
                objectCreationExpression.GetLocation());

            context.ReportDiagnostic(diagnostic);
        }
    }
}

