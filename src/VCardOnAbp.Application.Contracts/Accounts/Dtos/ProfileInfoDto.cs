namespace VCardOnAbp.Accounts.Dtos;
public record ProfileInfoDto(
    string Username,
    string Email,
    bool IsUnverifiedEmail,
    bool IsUnset2FA,
    decimal Balance,
    string RoleName
);