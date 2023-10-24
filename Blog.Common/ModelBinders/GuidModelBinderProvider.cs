using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Common.ModelBinders
{
    /// <summary>
    /// This class is the provider for the custom GUID model binder.
    /// </summary>
    public class GuidModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// This method implements a new model binder for GUID types only.
        /// </summary>
        /// <param name="context">Contains the model binder provider context.</param>
        /// <returns>Returns a model binder if the type is for GUID</returns>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder? result = null;

            if (context?.Metadata.ModelType == typeof(Guid))
            {
                result = new GuidModelBinder();
            }

            return result;
        }
    }
}