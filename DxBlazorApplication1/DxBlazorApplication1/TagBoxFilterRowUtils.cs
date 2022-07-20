using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;

namespace DxBlazorApplication1
{
    public class TagBoxFilterRowUtils
    {
        public static IEnumerable<string> GetValueByFunctionOperator(CriteriaOperator criteria, string fieldName)
        {
            var aggregateOperand = criteria as AggregateOperand;
            if (aggregateOperand.ReferenceEqualsNull() || aggregateOperand.AggregateType != Aggregate.Exists)
                return null;
            if (aggregateOperand.CollectionProperty is not OperandProperty operandProperty || operandProperty.PropertyName != fieldName)
                return null;
            if (aggregateOperand.Condition is not InOperator inOperator)
                return null;
            return inOperator.Operands.OfType<OperandValue>().Select(r => r.Value?.ToString());
        }

        public static CriteriaOperator CreateFilterCriteriaByValues(IEnumerable<string> values, string fieldName)
        {
            if (values.Count() == 0)
                return null;
            return new AggregateOperand(fieldName, Aggregate.Exists, new InOperator("", values));
        }
    }
}
