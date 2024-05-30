using Microsoft.AspNetCore.Mvc;


namespace Hospital.DAL.Common
{
    public static class ResponseHelper
    {
        public static async Task<IActionResult> CreateActionResult<T>(ApiResponse<T> response)
        {
            return (int)response.StatusCode switch
            {
                200 => new OkObjectResult(response),
                201 => new CreatedResult(string.Empty, response),
                204 => new NoContentResult(),
                400 => new BadRequestObjectResult(response),
                401 => new UnauthorizedObjectResult(response),
                403 => new ForbidResult(),
                404 => new NotFoundObjectResult(response),
                409 => new ConflictObjectResult(response),
                500 => new ObjectResult(response) { StatusCode = 500 },
                _ => new ObjectResult(response)
            };
        }
    }
}
