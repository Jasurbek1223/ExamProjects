using System.Linq.Expressions;

namespace LocalIdentity.SimpleInfra.Domain.Comparers;

public class KeySelectorExpressionComparer<TSource> : IComparer<Expression<Func<TSource, object>>>
{
    public int Compare(Expression<Func<TSource, object>>? x, Expression<Func<TSource, object>>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
    }
}