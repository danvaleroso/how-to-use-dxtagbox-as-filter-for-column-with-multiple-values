# Grid for Blazor - How to use the DxTagBox control as a filter for a column with multiple values

This example demonstrates how to use the [DxTagBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2) control in the [DxGridDataColumn.FilterRowCellTemplate](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGridDataColumn.FilterRowCellTemplate) to filter the column by multiple values.

![image](https://user-images.githubusercontent.com/69251191/180018055-298229e1-745b-46b7-984f-592c7d486e1e.png)

The main idea is to handle the [DxTagBox](http://docs.devexpress.devx/Blazor/DevExpress.Blazor.DxTagBox-2) [ValuesChanged](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2.ValuesChanged) event to set the "context.FilterCriteria" to a custom filter criteria. This custom filter criteria is created depending on the selected values of DxTagBox.

```razor
<DxGridDataColumn FieldName="SummaryString" >
    <FilterRowCellTemplate>
        @{
            var items = TagBoxFilterRowUtils.GetValueByFunctionOperator(context.FilterCriteria, nameof(WeatherForecast.Summary));
        }
        <DxTagBox TData="string"
                  TValue="string"
                  Data="Summaries"
                  Values="items"
                  ValuesChanged="(newValues) => { context.FilterCriteria = TagBoxFilterRowUtils.CreateFilterCriteriaByValues(newValues, nameof(WeatherForecast.Summary)); }" />
    </FilterRowCellTemplate>
</DxGridDataColumn>
```

```cs
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
```


## Files to Look At

* [Index.razor](./CS/DxBlazorApplication1/Pages/Index.razor)
* [TagBoxFilterRowUtils.cs](./CS/DxBlazorApplication1/TagBoxFilterRowUtils.cs)

