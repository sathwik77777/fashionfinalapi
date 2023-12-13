using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FashionHexa.Entities;
using FashionHexa.Services;
using FashionHexa.Models;
using FashionHexa.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using log4net;
using System.Runtime.ConstrainedExecution;

namespace FashionHexa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {

        private readonly ISellerService sellerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private ILogger<SellerController> logger;
        public SellerController(ISellerService sellerService, IMapper mapper, IConfiguration configuration, ILogger<SellerController> logger)
        {
            this.sellerService = sellerService;
            _mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpGet, Route("GetAllSellers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllSellers()
        {
            try
            {
                List<Seller> sellers = sellerService.GetAllSeller();
                List<SellerDTO> sellerDTO = _mapper.Map<List<SellerDTO>>(sellers);
                return StatusCode(200, sellers);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("RegisterSeller")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddSeller(Seller seller)
        {
            try
            {
                Seller _seller = _mapper.Map<Seller>(seller);
                sellerService.AddSeller(_seller);
                return StatusCode(200, _seller);


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditSeller")]
        [Authorize(Roles = "Seller")]
        public IActionResult EditSeller(Seller seller)
        {
            try
            {
                Seller _seller = _mapper.Map<Seller>(seller);
                sellerService.UpdaterSeller(_seller);
                return StatusCode(200, _seller);


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteSeller/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSeller(int userId)
        {
            try
            {
                sellerService.Deleteseller(userId);
                return StatusCode(200, new JsonResult($"User with Id {userId} is Deleted"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetSellerById/{userId}")]
        [Authorize(Roles ="Admin")]
        public IActionResult SellerById(int userId)
        {
            try
            {
                Seller seller = sellerService.GetSellerById(userId);
                SellerDTO sellerDTO = _mapper.Map<SellerDTO>(seller);
                if (seller != null)
                    return StatusCode(200, seller);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
