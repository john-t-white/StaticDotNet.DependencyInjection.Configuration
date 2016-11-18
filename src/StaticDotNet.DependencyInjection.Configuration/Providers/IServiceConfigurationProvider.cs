using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaticDotNet.DependencyInjection.Configuration.Providers
{
	/// <summary>
	/// Responsible for getting a collection of <see cref="IServiceConfiguration" /> to add services to the <see cref="IServiceCollection" />.
	/// </summary>
    public interface IServiceConfigurationProvider
    {
		/// <summary>
		/// Gets a collection of <see cref="IServiceConfiguration" />.
		/// </summary>
		/// <returns>A collection of <see cref="IServiceConfiguration" />.</returns>
		IEnumerable<IServiceConfiguration> GetConfigurations();
    }
}
