using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Services;

namespace MinhaPrimeiraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    // Guardando a conexão com o DB criado
    // private readonly AppDbContext _context;
    private readonly IProdutoService _service;

    // Construtor 
    public ProdutoController(IProdutoService service)
    {
        // _context = context;
        _service = service;
    }

    // Lista de Produtos (objetos)
    // private static readonly List<Produto> Produtos = new List<Produto>
    // {
    //     new Produto { Id = 1, Nome = "Caderno", Preco = 15.90m, Quantidade = 10 },
    //     new Produto { Id = 2, Nome = "Lápis", Preco = 2.50m, Quantidade = 25 },
    //     new Produto { Id = 3, Nome = "Borracha", Preco = 1.80m, Quantidade = 18 },
    //     new Produto { Id = 4, Nome = "Caneta", Preco = 4.20m, Quantidade = 30 },
    //     new Produto { Id = 5, Nome = "Mochila", Preco = 89.90m, Quantidade = 8 },
    //     new Produto { Id = 6, Nome = "Estojo", Preco = 22.00m, Quantidade = 12 },
    //     new Produto { Id = 7, Nome = "Apontador", Preco = 3.70m, Quantidade = 15 },
    //     new Produto { Id = 8, Nome = "Cola", Preco = 5.60m, Quantidade = 20 },
    //     new Produto { Id = 9, Nome = "Tesoura", Preco = 12.40m, Quantidade = 9 }
    // };

    [HttpGet]
    // ASYNC espera o resultado chegar para abrir o 'ENVELOPE'
    public async Task<IActionResult> ListarTodos()
    {
        // AWAIT vai esperar essa 'CONFERIDA' no DB para, somente assim, listar o conteúdo
        var produtos = await _service.ListarTodosAsync();
        return Ok(produtos);
    }

    // Recebendo parametro ID
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        if (id <= 0)
            return BadRequest("O ID deve ser maior que zero.");

        // return Ok($"Produto número {id}");

        // Posição - 1. Se for inputado 3, precisa retornar a Borracha que está na possição 2 do Indice -> id - 1
        // return Ok(Produtos[id - 1]);

        // Encontrando de forma ASSINCRONA -> FindAsync
        var produto = await _service.BuscarPorIdAsync(id);
        if (produto == null)
            return NotFound($"Produto com ID {id} não encontrado.");

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

        var novaTarefa = await _service.CriarAsync(produto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = novaTarefa.Id }, novaTarefa);
    
        // Conexão com o DB
        // _context.Produtos.Add(produto);

        // // Salvando os produtos na tabela
        // await _context.SaveChangesAsync();
        // return Ok($"Produto '{produto.Nome}' criado com sucesso!");
    }

    // Versão com Model 
    // [HttpPost]
    // public IActionResult Criar([FromBody] Produto produto)
    // {
    //     return Ok(produto);
    // }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Produto produtoAtualizado)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produto = await _service.AtualizarAsync(id, produtoAtualizado);

        if (produto == null)
            return NotFound($"Produto com o ID {id} não encontrado.");
        
        // produto.Nome = produtoAtualizado.Nome;
        // produto.Preco = produtoAtualizado.Preco;
        // produto.Quantidade = produtoAtualizado.Quantidade;

        // Salvando os produtos na tabela      
        // await _service.SaveChangesAsync();
        return Ok($"Produto com ID {id} atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        if (id <= 0)
        return BadRequest(new { mensagem = "O ID deve ser maior que zero." });
    
        // Buscando o Produto por ID  
        var removido = await _service.DeletarAsync(id);

        if (!removido)
            return NotFound($"Produto com o ID {id} não encontrado.");
        
        // Deletando todo o produto
        // _context.Produtos.Remove(produto);
        // await _context.SaveChangesAsync();

        // return Ok($"Produto com ID {id} deletado com sucesso!");
        return NoContent();
    }
}