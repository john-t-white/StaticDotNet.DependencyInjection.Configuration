using Microsoft.Extensions.DependencyInjection;
using StaticDotNet.DependencyInjection.Configuration.Providers;
using StaticDotNet.ParameterValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StaticDotNet.DependencyInjection.Configuration
{
	/// <summary>
	/// Addes methods to <see cref="IServiceCollection" /> to configure services through an <see cref="IServiceConfiguration" />.
	/// </summary>
    public static class ServiceCollectionExtensions
    {
		/// <summary>
		/// Invokes <see cref="IServiceConfiguration.ConfigureServices(IServiceCollection)" /> on all <see cref="IServiceConfiguration" /> from the <paramref name="assemblies" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add the services too.</param>
		/// <param name="assemblies">The collection of assemblies to search.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="assemblies" /> is null.</exception>
		public static void AddConfigurations( this IServiceCollection services, params Assembly[] assemblies )
		{
			Parameter.Validate( assemblies, nameof( assemblies ) )
				.IsNotNull();

			AssemblyServiceConfigurationProvider provider = new AssemblyServiceConfigurationProvider( assemblies );

			services.AddConfigurations( provider );
		}

		/// <summary>
		/// Invokes on all <see cref="IServiceConfiguration.ConfigureServices(IServiceCollection)" /> returned by the <paramref name="provider"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add the services too.</param>
		/// <param name="provider">The <see cref="IServiceConfigurationProvider" />.</param>
		/// /// <exception cref="ArgumentNullException">Thrown when <paramref name="provider" /> is null.</exception>
		public static void AddConfigurations( this IServiceCollection services, IServiceConfigurationProvider provider )
		{
			Parameter.Validate( provider, nameof( provider ) )
				.IsNotNull();

			services.AddConfigurations( provider.GetConfigurations().ToArray() );
		}

		/// <summary>
		/// Invokes <see cref="IServiceConfiguration.ConfigureServices(IServiceCollection)" /> on all of the <paramref name="configurations" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add the services too.</param>
		/// <param name="configurations">The list <see cref="IServiceConfiguration" /> to add.</param>
		/// /// <exception cref="ArgumentNullException">Thrown when <paramref name="configurations" /> is null.</exception>
		public static void AddConfigurations( this IServiceCollection services, params IServiceConfiguration[] configurations )
		{
			Parameter.Validate( services, nameof( services ) )
				.IsNotNull();

			Parameter.Validate( configurations, nameof( configurations ) )
				.IsNotNull();

			foreach( IServiceConfiguration currentConfiguration in configurations )
			{
				currentConfiguration.ConfigureServices( services );
			}
		}
    }
}
