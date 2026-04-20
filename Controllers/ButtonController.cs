using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webAPI_ASPNET.Models;
using webAPI_ASPNET.Repositorios;
using webAPI_ASPNET.Repositorios.Interfaces;

namespace webAPI_ASPNET.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ButtonController : ControllerBase
    {
        private readonly IButton _buttonRepositorio;

        public ButtonController(IButton buttonRepositorio)
        {
            _buttonRepositorio = buttonRepositorio;
        }

        [Authorize("perm")]
        [HttpGet]
        public async Task<ActionResult<List<Button>>> getAll()
        {
            List<Button> buttons = await _buttonRepositorio.getAll();
            return Ok(buttons);
        }

        [Authorize("perm")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Button>> getById(int id)
        {
            Button button = await _buttonRepositorio.getById(id);
            return Ok(button);
        }

        [Authorize("perm")]
        [HttpPost]
        public async Task<ActionResult<Button>> post([FromBody] Button buttonModel)
        {
            Button button = await _buttonRepositorio.post(buttonModel);
            return Ok(button);
        }

        [Authorize("perm")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Button>> put([FromBody] Button buttonModel, int id)
        {
            buttonModel.ID = id;
            Button button = await _buttonRepositorio.put(buttonModel, id);

            return Ok(button);
        }

        [Authorize("perm")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Button>> delete(int id)
        {
            bool apagado = await _buttonRepositorio.delete(id);
            return Ok(apagado);
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class ButtonRelationController : ControllerBase
    {
        private readonly IButtonRelation _buttonRelationRepositorio;

        public ButtonRelationController(IButtonRelation buttonRelationRepositorio)
        {
            _buttonRelationRepositorio = buttonRelationRepositorio;
        }

        [Authorize("perm")]
        [HttpGet]
        public async Task<ActionResult<List<ButtonRelation>>> getAll()
        {
            List<ButtonRelation> buttonRelation =  await _buttonRelationRepositorio.getAll();
            return Ok(buttonRelation);
        }

        [Authorize("perm")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ButtonRelation>> getById(int id)
        {
            ButtonRelation buttonRelation = await _buttonRelationRepositorio.getById(id);
            return Ok(buttonRelation);
        }

        [Authorize("perm")]
        [HttpGet("{idUser}/{idButton}")]
        public async Task<ActionResult<ButtonRelation>> getByCodUserAndCodButton(int idUser, int idButton)
        {
            ButtonRelation buttonRelation = await _buttonRelationRepositorio.getByCodUserAndCodButton(idUser, idButton);
            return Ok(buttonRelation);
        }

        [Authorize("perm")]
        [HttpGet("Permissions/{idUSER}")]
        public async Task<ActionResult<List<UserWithButton>>> getUserIButtonByCodUser(int idUSER)
        {
            List<UserWithButton> userButton = await _buttonRelationRepositorio.getUserIButtonByCodUser(idUSER);
            return Ok(userButton);
        }

        [Authorize("perm")]
        [HttpPost]
        public async Task<ActionResult<ButtonRelation>> post([FromBody] ButtonRelation buttonRelationModel)
        {
            ButtonRelation buttonRelation = await _buttonRelationRepositorio.post(buttonRelationModel);
            return Ok(buttonRelation);
        }

        [Authorize("perm")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ButtonRelation>> put([FromBody] ButtonRelation buttonRelationModel, int id)
        {
            ButtonRelation buttonRelation = await _buttonRelationRepositorio.put(buttonRelationModel, id);
            return Ok(buttonRelation);
        }

        [Authorize("perm")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ButtonRelation>> delete(int id)
        {
            bool apagado = await _buttonRelationRepositorio.delete(id);
            return Ok(apagado);
        }
    }
}
