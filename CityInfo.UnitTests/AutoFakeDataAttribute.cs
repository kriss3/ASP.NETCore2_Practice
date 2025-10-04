using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit3;

namespace CityInfo.UnitTests;
public sealed class AutoFakeDataAttribute : AutoDataAttribute
{
	public AutoFakeDataAttribute() : base(() =>
	{
		var f = new Fixture().Customize(new AutoFakeItEasyCustomization
		{
			ConfigureMembers = true // auto-props filled on fakes/POCOs
		});
		return f;
	})
	{ }
}

public sealed class InlineAutoFakeDataAttribute : InlineAutoDataAttribute 
{
	public InlineAutoFakeDataAttribute(params object[] values)
		: base(new AutoFakeDataAttribute(), values) { }
}
