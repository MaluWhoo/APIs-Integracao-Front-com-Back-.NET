import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { ProdutoService } from '../../services/produto';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

export interface Produto {
  id: number;
  nome: string;
  preco: number | null;
  quantidade: number | null;
}

@Component({
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  selector: 'app-produto-list',
  styleUrl: './produto-list.css',
  templateUrl: './produto-list.html',
})
export class ProdutoList {

  produtos = signal<Produto[]>([]);

  // Lista estática para o front-end
  // private produtosMock: Produto[] = [
  //   { id: 1, nome: 'Notebook Gamer', preco: 4500.00, quantidade: 10},
  //   { id: 2, nome: 'Mouse sem Fio', preco: 120.50, quantidade: 20 },
  //   { id: 3, nome: 'Teclado Mecânico', preco: 350.00, quantidade: 15},
  //   { id: 4, nome: 'Monitor 24"', preco: 800.00, quantidade: 15}
  // ];

  carregando = signal(false);

  novoNome = '';
  novoPreco: number | null = null;
  novoQuantidade: number | null = null;

  editNome = '';
  editPreco: number | null = null;
  editQuantidade: number | null = null;

  editandoIdBusca: number | null = null;
  editNomeBusca = '';
  editPrecoBusca: number | null = null;
  editQuantidadeBusca: number | null = null;

  salvando = signal(false);
  editandoId: number | null = null;

  idBusca: number | null = null;
  produtoEncontrado = signal<Produto | null>(null);
  buscandoId = signal(false);

  erroBuscar = signal('');
  erroAdicionar = signal('');
  erroEdicao = signal('');

  constructor(public produtoService: ProdutoService) { }

  // FORMULÁRIO
  formItem = new FormGroup({
    id: new FormControl(null, Validators.required),
    nome: new FormControl('', Validators.required),
    preco: new FormControl(null, Validators.required),
    quantidade: new FormControl(null, Validators.required),
  });

  ngOnInit(): void {
    this.carregarProdutos();
  }

  carregarProdutos(): void {
    // this.carregando.set(true);
    this.produtoService.listarProdutos().subscribe({
      next: (produtos) => {
        this.produtos.set(produtos);
        // this.carregando.set(false);
      },
      error: () => {
        // this.carregando.set(false);
      },
    });
  }

  buscarProdutoPorId(): void {
    const id = Number(this.idBusca);

    if (!id || id <= 0) {
      this.erroBuscar.set('Informe um ID válido.');
      this.produtoEncontrado.set(null);
      return;
    }

    this.erroBuscar.set('');
    this.produtoEncontrado.set(null);
    this.buscandoId.set(true);

    // NEXT & ERRO
    this.produtoService.consultarProdutoPorId(id).subscribe({
      next: (produto) => {
        this.produtoEncontrado.set(produto);
        this.buscandoId.set(false);
      },
      error: () => {
        this.erroBuscar.set(`Produto com ID ${this.idBusca} não encontrado.`);
        this.buscandoId.set(false);
      },
    });
  }

  adicionarProduto(): void {
    if (!this.novoNome.trim() || this.novoPreco === null || this.novoQuantidade === null) {
      this.erroAdicionar.set('Preencha todos os campos.');
      return;
    }

    this.erroAdicionar.set('');
    this.salvando.set(true);

    const produto = { nome: this.novoNome.trim(), preco: Number(this.novoPreco) || 0, quantidade: Number(this.novoQuantidade) || 0 };

    // ADICIONANDO UM NOVO PRODUTO
    this.produtoService.adicionarProduto(produto).subscribe({
      next: () => {
        this.novoNome = '';
        this.novoPreco = null;
        this.novoQuantidade = null;
        this.erroAdicionar.set('');
        this.salvando.set(false);
        this.carregarProdutos();
      },
      error: () => { 
        this.salvando.set(false); 
        this.erroAdicionar.set('Erro ao preencher. Preencha novamente.');
      },
    })
  }

  editarProdutoDaBusca(produto: Produto): void {
    this.editandoIdBusca = produto.id;
    this.editNomeBusca = produto.nome;
    this.editPrecoBusca = produto.preco;
    this.editQuantidadeBusca = produto.quantidade;
  }

  editarProduto(produto: Produto): void {
    this.editandoId = produto.id;
    this.editNome = produto.nome;
    this.editPreco = produto.preco;
    this.editQuantidade = produto.quantidade;
  }

  salvarEdicaoBuscar(): void {
    if (this.editandoIdBusca === null || !this.editNomeBusca.trim() || this.editPrecoBusca === null || this.editPrecoBusca <= 0 || this.editQuantidadeBusca === null || this.editQuantidadeBusca < 0) {
      return;
    }
    const id = this.editandoIdBusca;
    const produto = { nome: this.editNomeBusca.trim(), preco: this.editPrecoBusca, quantidade: this.editQuantidadeBusca };
    this.salvando.set(true);
    this.produtoService.atualizarProduto(id, produto).subscribe({
      next: () => {
        const atualizado: Produto = { id, ...produto };
        this.produtos.update((lista) => lista.map((item) => item.id === id ? atualizado : item));
        if (this.produtoEncontrado()?.id === id) this.produtoEncontrado.set(atualizado);
        this.cancelarEdicaoBusca();
        this.salvando.set(false);
      },
      error: () => { this.salvando.set(false); },
    });
  }

  salvarEdicao(): void {
    if (this.editandoId === null || !this.editNome.trim() || this.editPreco === null || this.editPreco <= 0 || this.editQuantidade === null || this.editQuantidade < 0) {
      return;
    }
    const id = this.editandoId;
    const produto = { nome: this.editNome.trim(), preco: this.editPreco, quantidade: this.editQuantidade };
    this.salvando.set(true);
    this.produtoService.atualizarProduto(id, produto).subscribe({
      next: () => {
        const atualizado: Produto = { id, ...produto };
        this.produtos.update((lista) => lista.map((item) => item.id === id ? atualizado : item));
        if (this.produtoEncontrado()?.id === id) this.produtoEncontrado.set(atualizado);
        this.cancelarEdicao();
        this.salvando.set(false);
      },
      error: () => { this.salvando.set(false); },
    });
  }

  cancelarEdicaoBusca(): void {
    this.editandoIdBusca = null;
    this.editNomeBusca = '';
    this.editPrecoBusca = null;
    this.editQuantidadeBusca = null;
  }

  cancelarEdicao(): void {
    this.editandoId = null;
    this.editNome = '';
    this.editPreco = null;
    this.editQuantidade = null;
  }

  removerProduto(id: number): void {
    this.produtoService.removerProduto(id).subscribe({
      next: () => {
        this.produtos.update((lista) => lista.filter((i) => i.id !== id));
        if (this.produtoEncontrado()?.id === id) {
          this.produtoEncontrado.set(null);
          this.idBusca = null;
        }

        if (this.editandoId === id) this.cancelarEdicao();
      },
    })
  }
}
