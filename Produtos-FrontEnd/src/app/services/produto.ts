import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../components/produto-list/produto-list';

@Injectable({
    providedIn: 'root',
})
export class ProdutoService {
    private readonly apiUrl = 'http://localhost:5027/api/produto';

    constructor(private readonly http: HttpClient) {}

    listarProdutos(): Observable<Produto[]> {
        return this.http.get<Produto[]>(this.apiUrl);
    }

    consultarProdutoPorId(id: number): Observable<Produto> {
        return this.http.get<Produto>(`${this.apiUrl}/${id}`);
    }

    adicionarProduto(produto: Omit<Produto, 'id'>): Observable<string> {
        return this.http.post(this.apiUrl, produto, { responseType: 'text' });
    }

    atualizarProduto(id: number, produto: Omit<Produto, 'id'>): Observable<string> {
        return this.http.put(`${this.apiUrl}/${id}`, produto, { responseType: 'text' });
    }

    removerProduto(id: number): Observable<string> {
        return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
    }
}
