using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ProdutoraController : ControllerBase {

    private FilmeContext _context;
    private IMapper _mapper;

    public ProdutoraController(FilmeContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona uma produtora ao banco de dados
    ///</summary>
    ///<param name="produtoraDto">Objeto com os campos necessários para inserção de uma produtora</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionarProdutora([FromBody] CreateProdutoraDto produtoraDto){

                Produtora produtora = _mapper.Map<Produtora>(produtoraDto);

                _context.Produtoras.Add(produtora);
                _context.SaveChanges();

                return CreatedAtAction(nameof(ExibeProdutora),new {id = produtora.Id},produtora);
            }       

    ///<summary>
    ///Exibe uma produtora salva no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadProdutoraDto> ExibeProdutora([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadProdutoraDto>>(_context.Produtoras.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Exibe uma produtora salva no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult ExibeProdutoraId(int id){
               var produtora =  _context.Produtoras.FirstOrDefault(produtora => produtora.Id == id);
               if(produtora == null) return NotFound();
               var produtoraDto = _mapper.Map<ReadProdutoraDto>(produtora);
                return Ok(produtoraDto);
            }      


    ///<summary>
    ///Altera todos os elementos de uma produtora no banco de dados
    ///</summary>
    ///<param name="produtoraDto">Objeto com os campos necessários para alteração de uma produtora</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaProdutora(int id, [FromBody] UpdateProdutoraDto produtoraDto){
                var produtora = _context.Produtoras.FirstOrDefault(produtora => produtora.Id == id);

                if(produtora == null) return NotFound();
                _mapper.Map(produtoraDto,produtora);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Deleta uma produtora no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaProdutora(int id){
                var produtora = _context.Produtoras.FirstOrDefault(produtora => produtora.Id == id);

                if(produtora == null) return NotFound();
                _context.Remove(produtora);
                _context.SaveChanges();
                return NoContent();
            }
}
