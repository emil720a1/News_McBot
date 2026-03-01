namespace Application.DTO_s.UserDetailsDto;

public record AddUserDto(
    Guid Id, 
    string Username, 
    Guid TelegramId);