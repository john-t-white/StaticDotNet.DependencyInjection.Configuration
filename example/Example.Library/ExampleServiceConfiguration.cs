using StaticDotNet.DependencyInjection.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Library
{
	public class ExampleServiceConfiguration
		: IServiceConfiguration
	{
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddTransient<IExampleDependency, ExampleDependency>();
		}
	}
}
