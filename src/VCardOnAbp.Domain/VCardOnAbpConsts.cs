using Volo.Abp.Identity;

namespace VCardOnAbp;

public static class VCardOnAbpConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;

    public const int CardActiveDays = 10;

    public const int MaxNameLength = 50;
    public const int MaxDescriptionLength = 500;

    public const decimal FronzeBalance = 50;
}
