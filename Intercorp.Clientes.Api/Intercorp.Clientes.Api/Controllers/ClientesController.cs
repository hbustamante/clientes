using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intercorp.Clientes.Application.ApplicationServices.ClientServices;
using Intercorp.Clientes.Application.Dto;
using Intercorp.Clientes.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intercorp.Clientes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly ICreateClientService _createClientService;

        public ClientesController(
            ICreateClientService createClientService)
        {
            this._createClientService = createClientService;
        }


        // POST: api/Clientes
        /// <summary>
        /// Agrega un nuevo cliente a la base de datos.
        /// </summary>
        /// <remarks>
        /// Intercorp - Endpoint de Entrada POST.
        /// </remarks>
        /// <param name="clientInfo">Objeto a crear a la BD.</param>          
        /// <response code="200">OK. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        /// <response code="500">InternalServerError. Ha sucedido un error interno.</response>
        [HttpPost]
        [Route("creacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ClientDto clientInfo)
        {
            try
            {
                if (clientInfo == null) return BadRequest();

                var client = clientInfo.Adapt<Client>();
                var result = await _createClientService.CreateClient(client);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener listado de clientes.
        /// </summary>
        /// <remarks>
        /// Intercorp - Endpoint GET.
        /// </remarks>
        /// <response code="200">OK. Obtención de listado correcto.</response>
        /// <response code="500">InternalServerError. Ha sucedido un error interno.</response>
        [HttpGet]
        [Route("listclientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _createClientService.GetClients();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener kpi de clientes.
        /// </summary>
        /// <remarks>
        /// Intercorp - Endpoint GET KPI.
        /// </remarks>
        /// <response code="200">OK. Obtención de listado correcto.</response>
        /// <response code="500">InternalServerError. Ha sucedido un error interno.</response>
        [HttpGet]
        [Route("kpideclientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKpi()
        {
            try
            {
                var result = await _createClientService.GetKpi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Obtener promedio de edad de clientes.
        /// </summary>
        /// <remarks>
        /// Intercorp - Endpoint GET promedio.
        /// </remarks>
        /// <response code="200">OK. Obtención de listado correcto.</response> 
        /// <response code="500">InternalServerError. Ha sucedido un error interno.</response>
        [HttpGet]
        [Route("Promedio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAverage()
        {
            try
            {
                var result = await _createClientService.GetAverage();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener desviación estándar de edad de clientes.
        /// </summary>
        /// <remarks>
        /// Intercorp - Endpoint GET desviación estándar.
        /// </remarks>
        /// <response code="200">OK. Obtención de listado correcto.</response> 
        /// <response code="500">InternalServerError. Ha sucedido un error interno.</response>
        [HttpGet]
        [Route("DesviacionEstandar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStandardDeviation()
        {
            try
            {
                var result = await _createClientService.GetStandardDeviation();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
