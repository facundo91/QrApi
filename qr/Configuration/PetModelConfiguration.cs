using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Configuration
{


    /// <summary>
    /// Represents the model configuration for orders.
    /// </summary>
    public class PetModelConfiguration : IModelConfiguration
    {
        /// <summary>
        /// Applies model configurations using the provided builder for the specified API version.
        /// </summary>
        /// <param name="builder">The <see cref="ODataModelBuilder">builder</see> used to apply configurations.</param>
        /// <param name="apiVersion">The <see cref="ApiVersion">API version</see> associated with the <paramref name="builder"/>.</param>
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var order = builder.EntitySet<PetResponse>("Pets").EntityType.HasKey(o => o.Id);
        }
    }
}
