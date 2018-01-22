﻿using System.Collections.Generic;
using System.Linq.Expressions;
using Zarf.Metadata.Entities;

namespace Zarf.Generators
{
    public interface ISQLGenerator
    {
        string Generate(Expression expression, List<DbParameter> parameters);

        string Generate(Expression expression);
    }
}