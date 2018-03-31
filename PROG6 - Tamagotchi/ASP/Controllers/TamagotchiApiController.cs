using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using ASP.TamagotchiService;

namespace ASP.Controllers
{
    public class TamagotchiApiController : ApiController
    {
        private readonly ITamagotchiService _service;

        public TamagotchiApiController(ITamagotchiService service)
        {
            _service = service;
        }

        // GET: api/TamagotchiApi/CurrentTamagotchi
        [HttpGet]
        public string CurrentTamagotchi()
        {
            return new JavaScriptSerializer().Serialize(_service.GetCurrentTamagotchi());
        }

        // GET: api/TamagotchiApi/DoAction
        [HttpGet]
        public string DoAction([FromUri] string action)
        {
            if (_service.IsCurrentlyRunningAnAction())
            {
                return "There's already an action running!";
            }

            return _service.DoAction(action).ToString();
        }

        // GET: api/TamagotchiApi/ToggleGameRule
        [HttpGet]
        public bool ToggleGameRule([FromUri] string rule)
        {
            return _service.ToggleRule(rule);
        }
    }
}
