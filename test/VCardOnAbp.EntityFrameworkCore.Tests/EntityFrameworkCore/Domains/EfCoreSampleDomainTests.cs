using VCardOnAbp.Samples;
using Xunit;

namespace VCardOnAbp.EntityFrameworkCore.Domains;

[Collection(VCardOnAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<VCardOnAbpEntityFrameworkCoreTestModule>
{

}
