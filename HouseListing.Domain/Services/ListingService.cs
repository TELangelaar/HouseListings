using FluentValidation;
using FluentValidation.Results;
using HouseListing.Domain.Events;
using HouseListing.Domain.Models;
using HouseListing.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace HouseListing.Domain.Services;

public class ListingService : IListingService
{
    private readonly ILogger<ListingService> _logger;
    private readonly IRepositoryWrapper _repository;

    public ListingService(ILogger<ListingService> logger, IRepositoryWrapper repository)
    {
        _logger = logger;
        _repository = repository;
    }

    private void SeedListingDb() { 
        var listings = new Listing[]
        {
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Lovely country side home with a 100 square meter footprint",
                Price = 75_000
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Gracious artistic studio",
                Price = 30_000
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Fantastic starter home in an upcoming area",
                Price = 275_000
            },
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "Sea-side Villa with 5 hectares of beach property.",
                Price = 500_000
            }
        };

        foreach (var listing in listings)
        {
            _logger.LogInformation("Adding listing {@listing}", listing);
            _repository.HouseListing.Create(listing);
        };
    }

    public bool Create(Listing listing)
    {
        var existing_listing = _repository.HouseListing.FindById(listing.Id);
        if (existing_listing is not null)
        {
            var message = $"A listing with id {listing.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(listing.Id, message));
        }

        _repository.HouseListing.Create(listing);
        return _repository.Save();
    }

    public ICollection<Listing> GetAll()
    {
        var listings = _repository.HouseListing.FindAll().ToList();
        if (listings.Count == 0)
        {
            _logger.LogInformation("No listings, seeding database");
            SeedListingDb();
            return _repository.HouseListing.FindAll().ToList();
        }

        return listings;
    }

    public Listing Get(Guid id)
    {
        var listing = _repository.HouseListing.FindById(id);

        if (listing is null)
        {
            _logger.LogWarning(LogEvents.GetListingFailed, "Listing does not exist");
            var message = $"A listing with id {id} does not exist";
            throw new ValidationException(message, GenerateValidationError(id, message));
        }
        _logger.LogInformation(LogEvents.GetListingSuccessful, "Listing succesfully retrieved {listing}", listing);

        return listing;
    }

    public bool Update(Listing listing)
    {
        var existing_listing = _repository.HouseListing.FindById(listing.Id);

        if (existing_listing is null)
        {
            var message = $"A listing with id {listing.Id} does not exist";
            throw new ValidationException(message, GenerateValidationError(listing.Id, message));
        }

        _repository.HouseListing.Update(listing);
        return _repository.Save();
    }

    public bool Delete(Guid id)
    {
        var existing_listing = _repository.HouseListing.FindById(id);

        if (existing_listing is null)
        {
            var message = $"A listing with id {id} does not exist";
            throw new ValidationException(message, GenerateValidationError(id, message));
        }

        _repository.HouseListing.Delete(existing_listing);
        return _repository.Save();
    }

    private static ValidationFailure[] GenerateValidationError(Guid id, string message)
    {
        return new[]
        {
            new ValidationFailure(nameof(Guid), message)
        };
    }
}
