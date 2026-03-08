namespace Application.DTO_s.UserDetailsDto;

public record AddUserDto(
    long Id, 
    string Username, 
    Guid TelegramId);