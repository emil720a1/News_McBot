using Application.DTO_s.UserDetailsDto;
using CSharpFunctionalExtensions;
using Shared;

namespace Application.UserService;

public interface IUserService
{
    Task<Result<UserDetailsDto, Failure>> AddUserAsync(AddUserDto addUserDto, CancellationToken cancellationToken);
    
    Task<Result<UserDetailsDto, Failure>> GetDetailsAsync(GetUserByIdDto getUserByIdDto, CancellationToken cancellationToken);
    
    Task<Result<bool, Failure>> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    
    Task<Result<bool,Failure>> DeleteAsync(DeleteUserDto deleteUserDto, CancellationToken cancellationToken);
}