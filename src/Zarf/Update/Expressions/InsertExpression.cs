﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Zarf.Metadata.Entities;

namespace Zarf.Query.Expressions
{
    public class InsertExpression : Expression
    {
        public override Type Type => typeof(void);

        public override ExpressionType NodeType => ExpressionType.Extension;

        public IEnumerable<DbParameter> DbParams { get; }

        public IEnumerable<string> Columns { get; }

        public Table Table { get; }

        public bool GenerateIdentity { get; }

        public InsertExpression(Table table, IEnumerable<DbParameter> parameters, IEnumerable<string> columns, bool generateIdentity = false)
        {
            Table = table;
            DbParams = parameters;
            Columns = columns;
            GenerateIdentity = generateIdentity;
        }
    }
}
