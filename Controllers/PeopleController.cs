using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_new_ember.Controllers
{
  [Route("api/[controller]")]
  public class PeopleController : JsonApiController<Person>

  {
    public PeopleController(
        IJsonApiContext jsonApiContext,
        IResourceService<Person> resourceService,
        ILoggerFactory loggerFactory)
        : base(jsonApiContext, resourceService, loggerFactory)
    { }
  }
}
