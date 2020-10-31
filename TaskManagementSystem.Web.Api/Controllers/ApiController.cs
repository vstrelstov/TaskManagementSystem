using System;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.BusinessObjects.DTO;
using TaskManagementSystem.BusinessObjects.Interfaces;

namespace TaskManagementSystem.Web.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public ApiController(ITasksService service)
        {
            _tasksService = service;
        }

        [HttpGet("[action]")]
        public JsonResult GetTasksHierarchy()
        {
            return new JsonResult(_tasksService.GetTasksHierarchy());
        }

        [HttpGet("[action]")]
        public JsonResult GetTaskById([FromQuery] Guid id)
        {
            var res = _tasksService.GetTaskById(id);
            return new JsonResult(res);
        }

        [HttpPost("[action]")]
        public IActionResult AddTask([FromBody] ManagedTaskDto newTask)
        {
            _tasksService.AddTask(newTask);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult UpdateTask([FromBody] ManagedTaskDto task)
        {
            _tasksService.UpdateTask(task);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult DeleteTask([FromBody] PrimitiveTypesAjaxWrapper<Guid> taskId)
        {
            _tasksService.DeleteTask(taskId.Value);
            return Ok();
        }
    }
}
