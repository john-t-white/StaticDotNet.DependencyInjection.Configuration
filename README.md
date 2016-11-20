## StaticDotNet.DependencyInjection.Configuration

StaticDotNet.DependencyInjection.Configuration provides a way to configure services outside of the Startup class.

## Supported Platforms
* .NET Core (.NET Standard 1.0 and higher)
* 4.6.1 .NET Framework

Please let me know other platforms you would like support for.


## Installation

Installation is done via NuGet:

    Install-Package StaticDotNet.DependencyInjection.Configuration

## Usage

### Service Configuration Class

Add a class that implements `StaticDotNet.DependencyInjection.Configuration.IServiceConfiguration` to your project.

```csharp
using StaticDotNet.DependencyInjection.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Library
{
	public class ExampleServiceConfiguration
		: IServiceConfiguration
	{
		public void ConfigureServices( IServiceCollection services )
		{
			//Configure your services here.
		}
	}
}
```

### Startup Class

Add the `StaticDotNet.DependencyInjection.Configuration` namespace

```csharp
using StaticDotNet.DependencyInjection.Configuration;
```

#### Adding Specific Configurations

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddConfigurations( new ExampleServiceConfiguration() );
}
```

#### Adding Configurations from Assemblies

```csharp
 public void ConfigureServices(IServiceCollection services)
{
	Assembly[] assemblies = new Assembly[]
	{
		// Add your assembiles here.
	};

	services.AddConfigurations( assemblies );
}
```

#### Adding Configurations using a Provider

Create a class that implements `StaticDotNet.DependencyInjection.Configuration.Providers.IServiceConfigurationProvider` and implement the `IServiceConfiguration.GetConfigurations` method to return an `IEnumerable<IServiceConfiguration>`.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	IServiceConfigurationProvider provider = new MyServiceConfigurationProvider();

	services.AddConfigurations( provider );
}
```