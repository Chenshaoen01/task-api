using Microsoft.OData.ModelBuilder;
using Microsoft.OData.Edm;
using Task.Application.Dtos.Task;

namespace TaskApi.OData;

public static class ODataEDMModelBuilder
{
    public static IEdmModel Build()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<TaskItemGet>("Tasks");

        return builder.GetEdmModel();
    }
}
