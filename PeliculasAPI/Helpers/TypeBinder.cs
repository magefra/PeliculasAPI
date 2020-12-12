using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public  Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if(proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var valorDeserializador = JsonConvert.DeserializeObject<T>(proveedorDeValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializador);

            }
            catch 
            {

                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor inválido para tipo List<int>");
            }

            return Task.CompletedTask;

        }
    }
}
