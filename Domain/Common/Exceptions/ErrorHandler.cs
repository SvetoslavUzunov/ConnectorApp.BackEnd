using Domain.Models.Errors;
using Microsoft.AspNetCore.Identity;

namespace Domain.Common.Exceptions;

public class ErrorHandler
{
    public static void ExecuteErrorHandler(IdentityResult identityResult)
    {
        var errorModel = new ErrorModel();

        errorModel.Errors.AddRange(identityResult.Errors.Select(x => x.Description));

        throw new ValidationException(errorModel.Errors);
    }
}
