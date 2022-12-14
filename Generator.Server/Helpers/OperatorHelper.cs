using Cysharp.Text;
using Generator.Shared.Enums;

namespace Generator.Server.Helpers;

public static class OperatorHelper
{
    public static string CreateStatement(this LogicalOperator @operator, string key)
    {
        if (@operator == LogicalOperator.Equal)
            return @operator.CreateStatement(key, null);

        if (@operator == LogicalOperator.LessOrEqual)
            return @operator.CreateStatement(key, null);

        if (@operator == LogicalOperator.GreaterOrEqual)
            return @operator.CreateStatement(key, null);

        if (@operator == LogicalOperator.Contains)
            return @operator.CreateStatement(key, null); 

        if (@operator == LogicalOperator.StartsWith)
            return @operator.CreateStatement(key, null);

        if (@operator == LogicalOperator.EndsWith)
            return @operator.CreateStatement(key, null);

 
        return default;
    }

    public static string CreateStatement(this LogicalOperator @operator, string key, params object[] values)
    {
        if (@operator == LogicalOperator.Equal) return $" {key} = @{key} ";

        if (@operator == LogicalOperator.LessOrEqual) return $" {key} <=  @{key}";

        if (@operator == LogicalOperator.GreaterOrEqual) return $" {key} >=  @{key}";

        if (@operator == LogicalOperator.Contains) return $" {key} LIKE '%'+@{key}+'%' ";

        if (@operator == LogicalOperator.StartsWith) return $" {key} LIKE ''+@{key}+'%' ";

        if (@operator == LogicalOperator.EndsWith) return $" {key} LIKE '%'+@{key}'' ";
 

        if (@operator == LogicalOperator.In)
        {
             var param = ZString.Join(", ", values.Select(x => $"@{x}").ToList());

            return @$" {key} IN ({param}) ";
        }
 
        return default;
 
    }

}
