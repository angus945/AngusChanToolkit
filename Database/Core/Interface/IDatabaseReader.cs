using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public interface IDatabaseReader<T> where T : new()
{
    T[] GetAll();
    T Get(Expression<Func<T, bool>> predicate);
}