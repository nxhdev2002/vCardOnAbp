using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCardOnAbp.Accounts.Dtos;
using VCardOnAbp.Localization;
using VCardOnAbp.Models;
using VCardOnAbp.Transactions;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
namespace VCardOnAbp.Accounts;

[ExposeServices(typeof(ProfileAppService), typeof(IProfileAppService))]
[Dependency(ReplaceServices = true)]
public class AccountsAppService : ProfileAppService, IAccountsAppService
{
    private readonly IRepository<UserTransaction, Guid> _userTransactionRepository;
    private readonly IRepository<Volo.Abp.Identity.IdentityRole, Guid> _identityRepo;
    private new readonly IStringLocalizer<VCardOnAbpResource> L;

    public AccountsAppService(
        IdentityUserManager userManager,
        IOptions<IdentityOptions> identityOptions,
        IRepository<UserTransaction, Guid> userTransactionRepository,
        IRepository<Volo.Abp.Identity.IdentityRole, Guid> identityRepo,
        IStringLocalizer<VCardOnAbpResource> localizer
    ) : base(userManager, identityOptions)
    {
        _userTransactionRepository = userTransactionRepository;
        _identityRepo = identityRepo;
        L = localizer;
    }

    public override Task<ProfileDto> UpdateAsync(UpdateProfileDto input)
    {
        input.ExtraProperties.Remove("Balance");
        return base.UpdateAsync(input);
    }

    [RemoteService(false)]
    public async Task<PagedResultDto<UserTransactionDto>> GetTransactionsAsync(GetUserTransactionInput input)
    {
        IOrderedQueryable<UserTransaction> query = (await _userTransactionRepository.GetQueryableAsync())
            .Where(x => x.UserId == CurrentUser.Id!.Value)
            .OrderByDescending(x => x.CreationTime);

        List<UserTransaction> data = await query
            .WhereIf(!string.IsNullOrEmpty(input.Filter), x => EF.Functions.Like(x.Description, $"%{input.Filter}%"))
            .PageBy(input)
            .ToListAsync();

        return new PagedResultDto<UserTransactionDto>(
            await query.CountAsync(),
            ObjectMapper.Map<List<UserTransaction>, List<UserTransactionDto>>(data)
        );
    }

    [RemoteService(false)]
    public async Task<ProfileInfoDto> GetProfileAsync()
    {
        var user = await UserManager.FindByIdAsync(CurrentUser.Id!.Value.ToString());

        if (user == null) throw new UserFriendlyException(L["NotFound", L["User"]]);

        var userRole = user.Roles.Select(x => x.RoleId);
        var role = await (await _identityRepo.GetQueryableAsync()).Where(x => userRole.Any(y => y == x.Id)).Select(x => x.Name).ToListAsync();

        return new ProfileInfoDto(
            user.UserName,
            user.Email,
            !user.EmailConfirmed,
            !user.TwoFactorEnabled,
            (decimal)(user.ExtraProperties[UserConsts.Balance] ?? 0),
            string.Join(',', role)
        );
    }

    [RemoteService(false)]
    public async Task<ResponseModel> CreateTwoFactorKeyAsync()
    {
        var user = await UserManager.FindByIdAsync(CurrentUser.Id!.Value.ToString());
        if (user == null) throw new UserFriendlyException(L["NotFound", L["CreateSecretKey"]]);

        if (user.TwoFactorEnabled) throw new UserFriendlyException(L["TwoFactorHasSetup"]);
        var secretKey = KeyGeneration.GenerateRandomKey(20); 
        var base32Key = Base32Encoding.ToString(secretKey); 

        var totpUri = $"otpauth://totp/{L["AppName"]}?secret={base32Key}&issuer={L["AppName"]}";
        using var generator = new QRCodeGenerator();
        var qrCodeData = generator.CreateQrCode(totpUri, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(20);
        user.ExtraProperties[UserConsts.TwoFactorSecret] = base32Key;

        return ResponseModel.SuccessResponse(L["SuccessToast", L["CreateSecretKey"]], new
        {
            Img = Convert.ToBase64String(qrCodeImage),
            Text = base32Key
        });
    }

    [RemoteService(false)]
    public async Task<ResponseModel> Verify2FA(string otp)
    {
        var user = await UserManager.FindByIdAsync(CurrentUser.Id!.Value.ToString());
        if (user == null) throw new UserFriendlyException(L["NotFound", L["User"]]);
        if (user.ExtraProperties[UserConsts.TwoFactorSecret] == null) throw new UserFriendlyException(L["FailToast", L["Confirm"]]);

        var secretKey = user.ExtraProperties[UserConsts.TwoFactorSecret]!.ToString();

        var totp = new Totp(Base32Encoding.ToBytes(secretKey));
        var isValid = totp.VerifyTotp(otp, out long timeStepMatched);

        if (isValid) await UserManager.SetTwoFactorEnabledAsync(user, true);
        return isValid ? ResponseModel.SuccessResponse(L["SuccessToast", L["Confirm"]]) : ResponseModel.ErrorResponse(L["FailToast", L["Confirm"]]);
    }
}
