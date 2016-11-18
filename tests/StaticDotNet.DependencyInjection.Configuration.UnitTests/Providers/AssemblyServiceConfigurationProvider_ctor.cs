using StaticDotNet.DependencyInjection.Configuration.Internal;
using StaticDotNet.DependencyInjection.Configuration.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace StaticDotNet.DependencyInjection.Configuration.UnitTests.Providers
{
    public class AssemblyServiceConfigurationProvider_ctor
    {
		[Fact]
		public void AssemblyServiceConfigurationProvider_ctor_InitializesCorrectly()
		{
			Assembly[] assemblies = Array.Empty<Assembly>();

			AssemblyServiceConfigurationProvider provider = new AssemblyServiceConfigurationProvider( assemblies );

			Assert.Same( assemblies, provider.Assemblies );
			Assert.IsType<ActivatorWrapper>( provider.Activator );
		}

		[Fact]
		public void AssemblyServiceConfigurationProvider_ctor_WithNullAssembliesThrowsArgumentNullException()
		{
			Assembly[] assemblies = null;

			Assert.Throws<ArgumentNullException>( nameof( assemblies ), () => new AssemblyServiceConfigurationProvider( assemblies ) );
		}
	}
}
