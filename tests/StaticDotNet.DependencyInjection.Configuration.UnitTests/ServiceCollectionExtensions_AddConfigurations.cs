using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using StaticDotNet.DependencyInjection.Configuration.Providers;
using System.Reflection;

namespace StaticDotNet.DependencyInjection.Configuration.UnitTests
{
    public class ServiceCollectionExtensions_AddConfigurations
    {
		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithConfigurationsInvokesCorrectly()
		{
			IServiceConfiguration[] configurations =
			{
				Substitute.For<IServiceConfiguration>(),
				Substitute.For<IServiceConfiguration>(),
				Substitute.For<IServiceConfiguration>()
			};

			ServiceCollection services = new ServiceCollection();

			services.AddConfigurations( configurations );

			foreach( IServiceConfiguration currentConfiguration in configurations )
			{
				currentConfiguration.Received().ConfigureServices( services );
			}
		}

		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithNullConfigurationsThrowsArgumentNullException()
		{
			IServiceConfiguration[] configurations = null;

			ServiceCollection services = new ServiceCollection();

			Assert.Throws<ArgumentNullException>( nameof( configurations ), () => services.AddConfigurations( configurations ) );
		}

		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithProviderInvokesCorrectly()
		{
			IServiceConfigurationProvider provider = Substitute.For<IServiceConfigurationProvider>();

			IServiceConfiguration[] configurations =
			{
				Substitute.For<IServiceConfiguration>(),
				Substitute.For<IServiceConfiguration>(),
				Substitute.For<IServiceConfiguration>()
			};

			provider.GetConfigurations()
				.Returns( configurations );

			ServiceCollection services = new ServiceCollection();

			services.AddConfigurations( provider );

			foreach( IServiceConfiguration currentConfiguration in configurations )
			{
				currentConfiguration.Received().ConfigureServices( services );
			}
		}

		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithNullProvideThrowsArgumentNullException()
		{
			IServiceConfigurationProvider provider = null;

			ServiceCollection services = new ServiceCollection();

			Assert.Throws<ArgumentNullException>( nameof( provider ), () => services.AddConfigurations( provider ) );
		}

		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithAssembliesInvokesCorrectly()
		{
			Assembly[] assemblies =
			{
				typeof( ServiceCollectionExtensions_AddConfigurations ).GetTypeInfo().Assembly
			};

			ServiceCollection services = new ServiceCollection();

			services.AddConfigurations( assemblies );

			Assert.True( services.Any( x => x.ServiceType == typeof( string ) ) );
			Assert.True( services.Any( x => x.ServiceType == typeof( object ) ) );
			Assert.True( services.Any( x => x.ServiceType == typeof( int[] ) ) );
		}

		[Fact]
		public void ServiceCollectionExtensions_AddConfigurations_WithNullAssembliesThrowsArgumentNullException()
		{
			Assembly[] assemblies = null;

			ServiceCollection services = new ServiceCollection();

			Assert.Throws<ArgumentNullException>( nameof( assemblies ), () => services.AddConfigurations( assemblies ) );
		}

		#region Test Classes

		public class StringServiceConfiguration
			: IServiceConfiguration
		{
			public void ConfigureServices( IServiceCollection services )
			{
				services.AddSingleton<string>( "Hello" );
			}
		}

		public class ObjectServiceConfiguration
			: IServiceConfiguration
		{
			public void ConfigureServices( IServiceCollection services )
			{
				services.AddSingleton<object>( new object() );
			}
		}

		public class InterviceConfiguration
			: IServiceConfiguration
		{
			public void ConfigureServices( IServiceCollection services )
			{
				services.AddSingleton<int[]>( new int[] { 1 } );
			}
		}

		#endregion
	}
}
