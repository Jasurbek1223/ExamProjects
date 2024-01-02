namespace LocalIdentity.SimpleInfra.Domain.Common.Query;

public interface IQueryConvertible<TSource>
{
    QuerySpecification<TSource> ToQuerySpecification();
}