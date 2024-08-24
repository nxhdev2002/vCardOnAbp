using Xunit;

namespace VCardOnAbp.EntityFrameworkCore;

[CollectionDefinition(VCardOnAbpTestConsts.CollectionDefinitionName)]
public class VCardOnAbpEntityFrameworkCoreCollection : ICollectionFixture<VCardOnAbpEntityFrameworkCoreFixture>
{

}
