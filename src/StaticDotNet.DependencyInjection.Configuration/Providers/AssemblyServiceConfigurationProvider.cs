using StaticDotNet.DependencyInjection.Configuration.Internal;
using StaticDotNet.ParameterValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StaticDotNet.DependencyInjection.Configuration.Providers
{
	/// <summary>
	/// Returns a collection of <see cref="IServiceConfiguration" /> from the a collection of assemblies.
	/// </summary>
	public class AssemblyServiceConfigurationProvider
		: IServiceConfigurationProvider
	{
		/// <summary>
		/// Instaniates and instance of <see cref="AssemblyServiceConfigurationProvider" />.
		/// </summary>
		/// <param name="assemblies">The collection of assemblies to search.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="assemblies" /> is null.</exception>
		public AssemblyServiceConfigurationProvider( Assembly[] assemblies )
			: this( assemblies, new ActivatorWrapper() )
		{ }

		/// <summary>
		/// Instaniates and instance of <see cref="AssemblyServiceConfigurationProvider" />.
		/// </summary>
		/// <param name="assemblies">The collection of assemblies to search.</param>
		/// <param name="activator">The activator responsible for instantiating objects.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="assemblies" /> or <paramref name="activator" /> is null.</exception>
		public AssemblyServiceConfigurationProvider( Assembly[] assemblies, IActivator activator )
		{
			Parameter.Validate( assemblies, nameof( assemblies ) )
				.IsNotNull();

			Parameter.Validate( activator, nameof( activator ) )
				.IsNotNull();

			this.Assemblies = assemblies;
			this.Activator = activator;
		}

		/// <summary>
		/// Gets the collection of assmeblies to search.
		/// </summary>
		public IEnumerable<Assembly> Assemblies { get; }

		/// <summary>
		/// Gets the activator responsible for creating objects.
		/// </summary>
		public IActivator Activator { get; }

		/// <summary>
		/// Gets a collection of <see cref="IServiceConfiguration" /> from the <see cref="AssemblyServiceConfigurationProvider.Assemblies" />.
		/// </summary>
		/// <returns>The collection of <see cref="IServiceConfiguration" /> from the <see cref="AssemblyServiceConfigurationProvider.Assemblies" />.</returns>
		public IEnumerable<IServiceConfiguration> GetConfigurations()
		{
			return this.Assemblies.SelectMany( x => x.DefinedTypes )
				.Select( x => x.AsType() )
				.Where( x => typeof( IServiceConfiguration ).IsAssignableFrom( x ) )
				.Select( x => ( IServiceConfiguration )this.Activator.CreateInstance( x ) );
		}
	}
}
