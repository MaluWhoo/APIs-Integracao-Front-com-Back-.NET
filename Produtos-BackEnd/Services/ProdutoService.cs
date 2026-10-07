using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Repositories;

namespace MinhaPrimeiraApi.Services;

public class ProdutoService : IProdutoService
{
    // AQUI ENTRA REGRAS DE NEGOCIO
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Produto>> ListarTodosAsync()
    {
        return await _repository.ListarTodosAsync();
    }

    public async Task<Produto?> BuscarPorIdAsync(int id)
    {
        return await _repository.BuscarPorIdAsync(id);
    }

    public async Task<Produto> CriarAsync(Produto produto)
    {
        produto.Nome = produto.Nome.Trim();

        if(produto.Preco < 0.01m)
            throw new ArgumentException("O preço deve ser maior que zero.");

        if(produto.Quantidade <= 0)
            throw new ArgumentException("A quantidade mínima de produtos é 1.");

        if (await _repository.ExisteNomeDuplicadoAsync(produto.Nome))
            throw new ArithmeticException("Já existe um produto cadastrado com o mesmo nome.");

        return await _repository.CriarAsync(produto);
    }

    public async Task<Produto?> AtualizarAsync(int id, Produto produtoAtualizado)
    {
        produtoAtualizado.Nome = produtoAtualizado.Nome.Trim();
        
        if (await _repository.ExisteNomeDuplicadoAsync(produtoAtualizado.Nome))
            throw new ArithmeticException("Já existe um produto cadastrado com o mesmo nome.");

        if(produtoAtualizado.Preco < 0.01m)
            throw new ArgumentException("O preço deve ser maior que zero.");

        if(produtoAtualizado.Quantidade <= 0)
            throw new ArgumentException("A quantidade mínima de produtos é 1.");

        return await _repository.AtualizarAsync(id, produtoAtualizado);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        return await _repository.DeletarAsync(id);
    }
}