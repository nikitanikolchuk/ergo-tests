using System.ComponentModel;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// An implementation of <c>ITestBuilderFactory</c>.
/// </summary>
/// <param name="nhptSectionBuilder">Section builder for Nine Hole Peg Test.</param>
/// <param name="pptSectionBuilder">Section builder for Purdue Pegboard Test.</param>
/// <param name="bbtSectionBuilder">Section builder for Box and Block Test.</param>
public class TestBuilderFactory(
    NhptTestSectionBuilder nhptSectionBuilder,
    PptTestSectionBuilder pptSectionBuilder,
    BbtTestSectionBuilder bbtSectionBuilder
) : ITestBuilderFactory
{
    public TestBuilder Create(TestType type)
    {
        var sectionBuilder = _getSectionBuilder(type);
        return new TestBuilder(sectionBuilder);
    }

    private ITestSectionBuilder _getSectionBuilder(TestType type) => type switch
    {
        TestType.Nhpt => nhptSectionBuilder,
        TestType.Ppt => pptSectionBuilder,
        TestType.Bbt => bbtSectionBuilder,
        _ => throw new InvalidEnumArgumentException(
            nameof(type),
            Convert.ToInt32(type),
            typeof(TestType)
        )
    };
}