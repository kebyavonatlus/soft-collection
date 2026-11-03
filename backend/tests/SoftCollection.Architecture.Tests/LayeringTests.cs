using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace SoftCollection.Architecture.Tests;

/// <summary>
/// Правила границ модульного монолита. По мере появления модулей (Modules/*)
/// сюда добавляются правила: модуль обращается к другому только через *.Contracts.
/// </summary>
public class LayeringTests
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            typeof(SoftCollection.SharedKernel.Finance.Money).Assembly,
            typeof(SoftCollection.Api.ApiAssemblyMarker).Assembly,
            typeof(SoftCollection.PublicGateway.PublicGatewayAssemblyMarker).Assembly,
            typeof(SoftCollection.Worker.WorkerAssemblyMarker).Assembly)
        .Build();

    private static readonly IObjectProvider<IType> SharedKernel =
        Types().That().ResideInNamespaceMatching(@"^SoftCollection\.SharedKernel(\..*)?$").As("SharedKernel");

    [Fact]
    public void SharedKernel_does_not_depend_on_hosts()
    {
        Types().That().Are(SharedKernel)
            .Should().NotDependOnAny(Types().That().ResideInNamespaceMatching(@"^SoftCollection\.(Api|Worker|PublicGateway)(\..*)?$"))
            .Check(Architecture);
    }

    [Fact]
    public void PublicGateway_does_not_depend_on_internal_Api()
    {
        // Публичный контур в DMZ не должен тянуть код внутреннего API.
        Types().That().ResideInNamespaceMatching(@"^SoftCollection\.PublicGateway(\..*)?$")
            .Should().NotDependOnAny(Types().That().ResideInNamespaceMatching(@"^SoftCollection\.Api(\..*)?$"))
            .Check(Architecture);
    }
}
