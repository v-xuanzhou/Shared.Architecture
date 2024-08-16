
using Microsoft.AspNetCore.Mvc;
using Shared.Architecture.Astro.Conditions.Parser;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using UserApi.Msic.CustomerException;

namespace MyAPI.Controller
{
    [Route("api/datatable")]
    [ApiController]
    public class DataTableController : ControllerBase
    {
        private IAstroTablePipelineExecuter _astroTablePipelneExecuter;
        private IConditionParser _conditionParser;

        public DataTableController(IAstroTablePipelineExecuter astroTablePipelneExecuter, IConditionParser conditionParser)
        {
            _astroTablePipelneExecuter = astroTablePipelneExecuter;
            _conditionParser = conditionParser;
        }

        [HttpGet,Route("/{tableName}")]
        public async Task<Astrotable> GetAstrotable(string tableName)
        {
            if (tableName == "1")
            {
                throw new MyException("pipeline not regist");
            }
            //var conditions = _conditionParser.GetQueryCondition(request);
            var request1 = new GetAstroTableRequest{ EntityDefinitionName = tableName};
            
            return await _astroTablePipelneExecuter.GetAstroTableAsync(request1);
        }
    }
}
