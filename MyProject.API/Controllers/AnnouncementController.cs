using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.DTOs;
using MyProject.Application.Helpers;
using MyProject.Application.Repositories;
using MyProject.Application.Repositories.Announcements;
using MyProject.Application.RequestParameters;
using MyProject.Application.RequestParameters.Wrapper;
using MyProject.Domain.Entities;
using MyProject.Domain.Entities.Identity;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementWriteRepository _annnounceWriteRepository;
        private readonly IAnnouncementReadRepository _announcementReadRepository;
        private readonly ImageManager _imageManager;
        private readonly UserManager<AppUser> _userManager;

        public AnnouncementController(IAnnouncementWriteRepository anWrite,
            IAnnouncementReadRepository anRead,
            ImageManager imageManager,
            UserManager<AppUser> userManager)
        {
            _annnounceWriteRepository = anWrite;
            _announcementReadRepository = anRead;
            _imageManager = imageManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Annoncements([FromQuery] GetAnnounceFilterParams filterParams)
        {
            int totalRecords;
            var ListAnnouncement = _announcementReadRepository.GetAnnouncements(filterParams, out totalRecords);
            return Ok(new PagedResponse<List<FilterAnnouncements>>(ListAnnouncement, filterParams.PageIndex, filterParams.PageSize, totalRecords));
        }
        [HttpGet("all")]
        public IActionResult Annoncements()
        {
            var ListAnnouncements = _announcementReadRepository.GetAll(tracking: false);
            return Ok(ListAnnouncements);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult> Details(long id)
        {
            var DetailsAnnouncement = _announcementReadRepository.GetWhere(x => x.Id == id, tracking: false);
            if (DetailsAnnouncement is null) return NotFound();
            var model = await DetailsAnnouncement.Select(x => new DetailsAnnouncementDto()
            {
                Id = x.Id,
                Make = x.Model.Make.MakeName,
                Model = x.Model.ModelName,
                Price = x.Price,
                Currencie = x.Currency.CurrencyCode,
                BanType = x.BanType.Ban,
                Year = x.Year,
                CarImageFiles = x.CarImageFiles.Select(x => x.Image).ToList(),
                RelaseDate = x.CreatedDate,
            }).ToListAsync();
            return Ok(new Response<List<DetailsAnnouncementDto>>(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAnnouncementDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<string> imageNames = _imageManager.UploadedFile(model.images);
            var userId = _userManager.GetUserId(User);

            ICollection<CarImageFile> carImageFiles = imageNames.Select(x => new CarImageFile()
            {
                Image = x,
            }).ToList();
            var Announcement = new Announcement()
            {
                AppUserId = userId,
                BanTypeId = model.BanTypeId,
                CarImageFiles = carImageFiles,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Price = model.Price,
                ModelId = model.ModelId,
                CurrencyId = model.CurrencyId,
                Year = (int)model.Year
            };
            await _annnounceWriteRepository.AddAsync(Announcement);
            await _annnounceWriteRepository.SaveAsync();
            return Ok(new Response<CreateAnnouncementDto>(model));
        }

    }
}
