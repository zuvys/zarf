﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Zarf.Query.Expressions;

namespace Zarf.Query.Handlers.NodeTypes.MethodCalls
{
    internal class DistinctNodeHandler : MethodNodeHandler
    {
        public static IEnumerable<MethodInfo> SupprotedMethods { get; }

        static DistinctNodeHandler()
        {
            SupprotedMethods = ReflectionUtil.QueryableMethods.Where(item => item.Name == "Distinct");
        }

        public DistinctNodeHandler(IQueryContext queryContext, IQueryCompiler queryCompiper) : base(queryContext, queryCompiper)
        {

        }

        public override SelectExpression HandleNode(SelectExpression select, Expression exp, MethodInfo method)
        {
            Utils.CheckNull(select, "query");

            select.IsDistinct = true;

            return select;
        }
    }
}
