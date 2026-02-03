
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestApi.Data;
using RestApi.Models;

namespace RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<Data.RestApiDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Message for my self to show its running if i visit root
            app.MapGet("/", () => Results.Ok("REST API is running"));

            // Get all people (Id, Name, Lastname, phonenumber
            app.MapGet("/api/people", async (RestApiDBContext db) =>
            {
                return await db.People
                    .Select(p => new
                    {
                        p.PersonId,
                        p.FirstName,
                        p.LastName,
                        p.Phone
                    })
                    .ToListAsync();
            });

            // Get interests for specifik person
            app.MapGet("/api/people/{personId:int}/interests", async (RestApiDBContext db, int personId) =>
            {
                return await db.PersonInterests
                    .Where(pi => pi.PersonId == personId)
                    .Select(pi => new
                    {
                        pi.InterestId,
                        pi.Interest.Title,
                        pi.Interest.Description
                    })
                    .ToListAsync();
            });

            // Get all links connected to a person
            app.MapGet("/api/people/{personId:int}/links", async (RestApiDBContext db, int personId) =>
            {
                return await db.PersonInterestLinks
                    .Where(l => l.PersonId == personId)
                    .Select(l => new
                    {
                        l.PersonInterestLinkId,
                        l.InterestId,
                        l.Url,
                        l.Note
                    })
                    .ToListAsync();
            });

            // Connect a person to an interest
            app.MapPost("/api/people/{personId:int}/interests/{interestId:int}",
                async (RestApiDBContext db, int personId, int interestId) =>
                {
                    db.PersonInterests.Add(new PersonInterest
                    {
                        PersonId = personId,
                        InterestId = interestId
                    });

                    await db.SaveChangesAsync();
                    return Results.StatusCode(201);
                });

            // Add new link for person+interest
            app.MapPost("/api/people/{personId:int}/interests/{interestId:int}/links", 
            async (RestApiDBContext db, int personId, int interestId, CreateLinkDto dto) =>
            {
                db.PersonInterestLinks.Add(new PersonInterestLink
                {
                    PersonId = personId,
                    InterestId = interestId,
                    Url = dto.Url,
                    Note = dto.Note
                });

                await db.SaveChangesAsync();
                return Results.StatusCode(201);
            });

            app.Run();
        }
    }

    // Data Transfer Object (DTO) so the fk/pk is not sent (good practice apparently)
    public record CreateLinkDto(string Url, string? Note);
}
