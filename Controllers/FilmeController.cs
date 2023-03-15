using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase {

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    ///<summary>
    ///Adiciona um filme ao banco de dados
    ///</summary>
    ///<param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    ///<returns>IActionResult</returns>
    ///<response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionarFIlme([FromBody] CreateFilmeDto filmeDto){

                Filme filme = _mapper.Map<Filme>(filmeDto);

                _context.Filmes.Add(filme);
                _context.SaveChanges();

                return CreatedAtAction(nameof(RecuperaFilme),new {id = filme.Id},filme);
            }       

    ///<summary>
    ///Recupera um filme salvo no banco de dados
    ///</summary>
    ///<returns>IEnumerable</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet]
            public IEnumerable<ReadFilmeDto> RecuperaFilme([FromQuery] int skip = 0,[FromQuery] int take = 50){

               return  _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
            }  

    ///<summary>
    ///Recupera um filme salvo no banco de dados através do id
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="200">Caso operação seja feita com sucesso</response>

            [HttpGet("{id}")]
            public IActionResult RecuperaFilmeId(int id){
               var filme =  _context.Filmes.FirstOrDefault(filme => filme.Id == id);
               if(filme == null) return NotFound();
               var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }      


    ///<summary>
    ///Altera todos os elementos de um filme no banco de dados
    ///</summary>
    ///<param name="filmeDto">Objeto com os campos necessários para alteração de um filme</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPut("{id}")]
            public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto){
                var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

                if(filme == null) return NotFound();
                _mapper.Map(filmeDto,filme);
                _context.SaveChanges();
                return NoContent();
            }

    ///<summary>
    ///Altera um elemento em específico de um filme no banco de dados
    ///</summary>
    ///<param name="id">Objeto com os campos necessários para alteração de um filme</param>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso alteração seja feita com sucesso</response>

            [HttpPatch("{id}")]

            public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto>patch){
                var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

                if(filme == null) return NotFound();

                var FilmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
                patch.ApplyTo(FilmeParaAtualizar,ModelState);

                if(!TryValidateModel(FilmeParaAtualizar)){
                    return ValidationProblem(ModelState);
                }

                _mapper.Map(FilmeParaAtualizar,filme);
                _context.SaveChanges();
                return NoContent();
           
            }

    ///<summary>
    ///Deleta um filme no banco de dados
    ///</summary>
    ///<returns>IActionResult</returns>
    ///<response code="204">Caso a exclusão seja feita com sucesso</response>

            [HttpDelete("{id}")]
            public IActionResult DeletaFilme(int id){
                var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

                if(filme == null) return NotFound();
                _context.Remove(filme);
                _context.SaveChanges();
                return NoContent();
            }
}
