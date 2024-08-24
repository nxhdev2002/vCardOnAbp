using VCardOnAbp.Samples;
using Xunit;

namespace VCardOnAbp.EntityFrameworkCore.Applications;

[Collection(VCardOnAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<VCardOnAbpEntityFrameworkCoreTestModule>
{

}
