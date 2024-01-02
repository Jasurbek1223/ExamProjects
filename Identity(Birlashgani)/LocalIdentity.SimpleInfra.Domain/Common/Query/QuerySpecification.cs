using System.Linq.Expressions;
using LocalIdentity.SimpleInfra.Domain.Common.Caching;
using LocalIdentity.SimpleInfra.Domain.Comparers;
using Microsoft.EntityFrameworkCore.Query;

namespace LocalIdentity.SimpleInfra.Domain.Common.Query;

public class QuerySpecification<TSource>(uint pageSize, uint pageToken, bool asNoTracking, int? filterHashCode = default): ICacheModel
{
     public List<Expression<Func<TSource, bool>>> FilteringOptions { get; } = [];
     
     public List<(Expression<Func<TSource, object>> KeySelector, bool IsAscending)> OrderingOptions { get; } = [];

     public List<Expression<Func<TSource, object>>> IncludingOptions { get; } = [];

     public FilterPagination PaginationOptions { get; } = new()
     {
          PageSize = pageSize,
          PageToken = pageToken
     };

     public bool AsNoTracking { get; } = asNoTracking;

     public int? FilterHashCode { get; } = filterHashCode;

     public string CacheKey => $"{typeof(TSource).Name}_{GetHashCode()}";

     public override int GetHashCode()
     {
          if (FilterHashCode is not null) return FilterHashCode.Value;

          var hashCode = new HashCode();
          var expressionEqualityComparer = ExpressionEqualityComparer.Instance; 

          foreach (var filteringExpression in FilteringOptions.Order(new PredicateExpressionComparer<TSource>()))
               hashCode.Add(expressionEqualityComparer.GetHashCode(filteringExpression));

          foreach (var includeExpression in IncludingOptions.Order(new KeySelectorExpressionComparer<TSource>()))
               hashCode.Add(expressionEqualityComparer.GetHashCode(includeExpression));

          foreach (var orderingExpression in OrderingOptions)
               hashCode.Add(expressionEqualityComparer.GetHashCode(orderingExpression.KeySelector));
          
          hashCode.Add(PaginationOptions);

          return hashCode.ToHashCode();
     }

     public override bool Equals(object? obj)
     {
          return obj is QuerySpecification<TSource> querySpecification &&
                 querySpecification.GetHashCode() == GetHashCode();
     }
}