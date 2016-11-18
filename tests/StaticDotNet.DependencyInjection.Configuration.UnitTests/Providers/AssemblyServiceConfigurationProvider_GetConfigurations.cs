using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Reflection;
using StaticDotNet.DependencyInjection.Configuration.Providers;
using NSubstitute;
using StaticDotNet.DependencyInjection.Configuration.Internal;

namespace StaticDotNet.DependencyInjection.Configuration.UnitTests.Providers
{
	public class AssemblyServiceConfigurationProvider_GetConfigurations
	{
		[Fact]
		public void AssemblyServiceConfigurationProvider_GetConfigurations_ReturnsCorrectly()
		{
			IServiceConfiguration configuration = Substitute.For<IServiceConfiguration>();

			Assembly[] assemblies =
			{
				configuration.GetType().GetTypeInfo().Assembly
			};

			IActivator activator = Substitute.For<IActivator>();

			activator.CreateInstance( configuration.GetType() )
				.Returns( configuration );

			AssemblyServiceConfigurationProvider provider = new AssemblyServiceConfigurationProvider( assemblies, activator );

			IEnumerable<IServiceConfiguration> result = provider.GetConfigurations();

			Assert.Contains( result, x => x.GetType() == configuration.GetType() );
		}
	}
}
