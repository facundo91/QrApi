using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using qr.Contracts.v1;
using qr.Contracts.v1.Requests;
using qr.Contracts.v1.Responses;
using qr.Domain;
using qr.Services;

namespace qr.Controllers.v1
{
    public class QrsController : Controller
    {
        private readonly IQrService _qrService;
        private readonly IMapper _mapper;

        public QrsController(IQrService qrService, IMapper mapper)
        {
            _qrService = qrService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Qrs.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _qrService.GetQrsAsync());
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid qrId)
        {
            var qr = await _qrService.GetQrByIdAsync(qrId);
            if (qr == null) return NotFound();
            var qrResponse = _mapper.Map<QrResponse>(qr);
            return Ok(qrResponse);
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        public async Task<IActionResult> Create([FromBody] CreateQrRequest qrRequest)
        {
            var qr = _mapper.Map<Qr>(qrRequest);

            var qrCreated = await _qrService.CreateQrAsync(qr);

            if (qrCreated == null) return BadRequest();

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Qrs.Get.Replace("{qrId}", qrCreated.Id.ToString());

            var qrResponse = _mapper.Map<QrResponse>(qrCreated);
            return Created(locationUri, qrResponse);
        }

        [HttpPut(ApiRoutes.Qrs.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid qrId, [FromBody] UpdateQrRequest qrRequest)
        {
            var qr = _mapper.Map<Qr>(qrRequest);
            var qrUpdated = await _qrService.UpdateQrAsync(qr);
            if (!qrUpdated) return NotFound();
            return Ok(_mapper.Map<QrResponse>(qr));
        }

        [HttpDelete(ApiRoutes.Qrs.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid qrId)
        {
            var updated = await _qrService.DeleteQrAsync(qrId);
            if (updated) return NoContent();
            return NotFound();
        }
    }
}
