using Example.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.WebApplication.Controllers
{
	[Route( "Example" )]
    public class ExampleController
		: Controller
    {
		public ExampleController( IExampleDependency exampleDependency )
		{
			this.ExampleDependency = exampleDependency;
		}

		public IExampleDependency ExampleDependency { get; }

		public IActionResult Index()
		{
			this.ViewBag.ExampleDependency = this.ExampleDependency;

			return this.View();
		}
    }
}
