using backend.Models;
using backend.Services;

namespace backend.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users", async (UserService userService) =>
            {
                var users = await userService.GetAllUsersAsync();
                return Results.Ok(users);
            });

            app.MapGet("/users/{id}", async (UserService userService, int id) =>
            {
                var user = await userService.GetUserByIdAsync(id);
                return user is null ? Results.NotFound() : Results.Ok(user);
            });

            app.MapPost("/users", async (UserService userService, Users user) =>
            {
                var createdUser = await userService.CreateUserAsync(user);
                return Results.Created($"/users/{createdUser.Id}", createdUser);
            });

            app.MapPut("/users/{id}", async (UserService userService, int id, Users updatedUser) =>
            {
                var user = await userService.UpdateUserAsync(id, updatedUser);
                return user is null ? Results.NotFound() : Results.Ok(user);
            });

            app.MapDelete("/users/{id}", async (UserService userService, int id) =>
            {
                var deleted = await userService.DeleteUserAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
