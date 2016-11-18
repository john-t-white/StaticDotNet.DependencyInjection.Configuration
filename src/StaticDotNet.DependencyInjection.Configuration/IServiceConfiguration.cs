using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace StaticDotNet.DependencyInjection.Configuration
{
	/// <summary>
	/// Defines a class used to configure services.
	/// </summary>
    public interface IServiceConfiguration
    {
		/// <summary>
		/// Adds services to a <see cref="IServiceCollection" />.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
		void ConfigureServices( IServiceCollection services );
    }
}
