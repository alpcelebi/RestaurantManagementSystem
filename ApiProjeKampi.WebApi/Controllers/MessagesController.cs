using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;
        public MessagesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            try
            {
                var value = _context.Messages.ToList();
                return Ok(_mapper.Map<List<ResultMessageDto>>(value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            try
            {
                if (createMessageDto == null)
                {
                    return BadRequest("Mesaj bilgileri boş olamaz.");
                }

                var value = _mapper.Map<Message>(createMessageDto);
                _context.Messages.Add(value);
                _context.SaveChanges();
                return Ok("Mesaj Ekleme İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Geçersiz ID.");
                }

                var value = _context.Messages.Find(id);
                if (value == null)
                {
                    return NotFound("Mesaj bulunamadı.");
                }

                _context.Messages.Remove(value);
                _context.SaveChanges();
                return Ok("Mesaj Silme İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }

        [HttpGet("GetMessage")]
        public IActionResult GetMessage(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Geçersiz ID.");
                }

                var value = _context.Messages.Find(id);
                if (value == null)
                {
                    return NotFound("Mesaj bulunamadı.");
                }

                return Ok(_mapper.Map<GetByIdMessageDto>(value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            try
            {
                if (updateMessageDto == null)
                {
                    return BadRequest("Güncellenecek mesaj bilgileri boş olamaz.");
                }

                if (updateMessageDto.MessageId <= 0)
                {
                    return BadRequest("Geçersiz mesaj ID.");
                }

                var existingMessage = _context.Messages.Find(updateMessageDto.MessageId);
                if (existingMessage == null)
                {
                    return NotFound("Güncellenecek mesaj bulunamadı.");
                }

                var value = _mapper.Map<Message>(updateMessageDto);
                _context.Messages.Update(value);
                _context.SaveChanges();
                return Ok("Mesaj Güncelleme İşlemi Başarılı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }

        [HttpGet("MessageListByIsReadFalse")]
        public IActionResult MessageListByIsReadFalse()
        {
            try
            {
                var value = _context.Messages.Where(x => x.IsRead == false).ToList();
                return Ok(_mapper.Map<List<ResultMessageDto>>(value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "İç sunucu hatası: " + ex.Message);
            }
        }
    }
}
