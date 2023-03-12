using FluentValidation;
using FluentValidation.Results;
using HouseListing.Domain.Events;
using HouseListing.Domain.Models;
using HouseListing.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace HouseListing.Domain.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IRepositoryWrapper _repository;

    public UserService(ILogger<UserService> logger, IRepositoryWrapper repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public bool Create(User user)
    {
        var existing_user = _repository.User.FindById(user.Id);
        if (existing_user is not null)
        {
            var message = $"A user with id {user.Id} already exists";
            throw new ValidationException(message, GenerateValidationError(user.Id, message));
        }

        _repository.User.Create(user);
        return _repository.Save();
    }

    public User Get(Guid id)
    {
        var user = _repository.User.FindById(id);

        if (user is null)
        {
            _logger.LogWarning(LogEvents.GetUserFailed, "Failed to retrieve");
            var message = $"A user with id {id} does not exist";
            throw new ValidationException(message, GenerateValidationError(id, message));
        }
        _logger.LogInformation(LogEvents.GetUserSuccessful, "Succesfully retrieved {@user}", user);

        return user;
    }

    public ICollection<User> GetAll()
    {
        var users = _repository.User.FindAll().ToList();

        if (users.Count == 0)
        {
            _logger.LogWarning(LogEvents.GetUserFailed, "Failed to retrieve any users");
            return new List<User>();
        }
        _logger.LogInformation(LogEvents.GetUserSuccessful, "Succesfully retrieved all users: {@user}", users);

        return users;
    }

    public bool Update(User user)
    {
        var existing_user = _repository.User.FindById(user.Id);

        if (existing_user is null)
        {
            var message = $"A user with id {user.Id} does not exist";
            throw new ValidationException(message, GenerateValidationError(user.Id, message));
        }

        _repository.User.Update(user);
        return _repository.Save();
    }

    public bool Delete(Guid id)
    {
        var user = _repository.User.FindById(id);

        if (user is null)
        {
            var message = $"A user with id {id} does not exist";
            throw new ValidationException(message, GenerateValidationError(id, message));
        }

        _repository.User.Delete(user);
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
