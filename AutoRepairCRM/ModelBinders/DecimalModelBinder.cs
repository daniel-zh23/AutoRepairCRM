using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutoRepairCRM.ModelBinders;

public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ValueProviderResult valueResult = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
        {
            decimal actualValue = 0m;
            bool isSuccess = false;

            try
            {
                string value = valueResult.FirstValue;
                value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                actualValue = Convert.ToDecimal(value, CultureInfo.CurrentCulture);
                isSuccess = true;
            }
            catch (FormatException fe)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
            }

            if (isSuccess)
            {
                bindingContext.Result = ModelBindingResult.Success(actualValue);
            }
        }
        
        return Task.CompletedTask;
    }
}